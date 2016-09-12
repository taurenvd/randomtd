using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections.Generic;
using System.IO;
using System.Xml.Serialization;

public class UI : MonoBehaviour
{

    Color colorlerp;

    public GameObject blog;
    GameObject target = null;
    public GameObject[] types;
    GameObject buf = null;
    public GameObject TowerType;

    public Transform statusPanel;
    public Transform option;
    public Transform logPanel;
    public Transform CenterPanel;
    public Transform buildSpot;
    public Transform Content;

    public Text mobsLeft;
    public Text centerText;
    public Text randomText;
    public Text livesT;
    public Text status;
    public Text statusE;

    public Material defmat;
    public Material greenmat;

    public Ray ray;

    RaycastHit buildHit;
    RaycastHit statusHit;

    public string info;
    public string playerName;

    public float timeToWave;
    public float constTime = 4;
    public float creepsOnWave;
    public float deadCreeps = 0;
    public float messageDelay = 4;
    public float randomTimer = 300;

    public int rand;
    public int curType;
    public int gold;
    public int price;
    public int waveCount = 1;
    public int lives = 50;
    public int score;

    public bool wait = false;
    public bool chosePlatform = false;
    bool gameOver = false;
    int inverse =0;

    public Button menu;
    public Button restart;
    public Button resume;
    public Button exit;
    public Button buyLife;
    public Button build;
    public Button random;
    public Button shop;
    public Button upgrade;
    public Button sell;
    public Button SaveB;
    public Button LoadB;
    public Button options;

    public List<GameObject> platformsL = new List<GameObject>();
    public List<GameObject> towers;

    public List<Vector3> towerPosL;
    public List<string> towerTypesL;
    public List<string> platformNameL;
    public List<string> parent;
 
    public ColorBlock z = new ColorBlock();

    public GameObject settings;

  //---------------------------------------------------------------------------------------
  // UNITY Section
    void Start()
    {
        SaveB.onClick.AddListener(OnSafeClick);

        StartCoroutine(RandPrice());
        TowerTypeDownload(TowerType);
        StartCoroutine(Message(centerText, "Wave " + waveCount));
        mobsLeft.text = "  Wave:Creeps count " + creepsOnWave + "\n  Creeps left: " + (creepsOnWave - deadCreeps);
        livesT.text = "Lives: " + lives + "  ";      
        foreach (var item in GameObject.FindGameObjectsWithTag("Platforms"))
        {
            platformNameL.Add(item.name);
            platformsL.Add(item);
        }
        statusPanel.gameObject.SetActive(false);
        z.pressedColor = shop.colors.pressedColor;
        z.colorMultiplier = shop.colors.colorMultiplier;
        StartCoroutine(ColorChanger(random, Color.yellow, Color.red, 5f));

    }
    void Update()
    {

        if (options.isActiveAndEnabled==false&&settings.GetComponentInChildren<Text>().isActiveAndEnabled)
        {
            settings.SetActive(false);
        }
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (inverse % 2 == 0)
            {
                Time.timeScale = 0f;
                StartCoroutine(Message(centerText, "Pause", 0.1f));
                inverse++;
            }
            else
            {
                Time.timeScale = 1f;
                StartCoroutine(Message(centerText, "", 0.1f));
                inverse++;
            }
        }
     

        var mas =new List<GameObject>( GameObject.FindGameObjectsWithTag("Updated"));
        foreach (var item in mas)
        {
           string[] after= item.gameObject.GetComponentInChildren<Text>().text.Split(' ');
            item.gameObject.GetComponentInChildren<Text>().text=after[0]+" "  + price;
        
        }
        random.GetComponentInChildren<Text>().text = "Random: " + price;
        RandomEvent();
        LabelManager();
        WaveBreak();
        StatusBar();
       
    }

    //----------------------------------------------------------------------------------------
    //BUTTONS SECTIONS
    #region OnButtonClick 
    public void OnRandomClick()
    {
        if (gold < price)
        {
            StartCoroutine(Message(GetComponentInParent<Text>(), "Not Enough Cash", 3));
            return;
        }
        else gold -= price;
        rand = Random.Range(0, 4);
        switch (rand)
        {
            case 1: RandomBuild(); break;
            case 2: RandomSell(); break;
            default: StartCoroutine(Message(GetComponentInParent<Text>(), "Nothing", 3)); break;
        }

    }
    public void OnShopClick()
    {

        SetActiveRec(build.gameObject,buyLife.gameObject,random.gameObject);
    }
    public void OnMenuClick()
    {
        Time.timeScale = 0f;
        StartCoroutine(Message(centerText, "Pause", 0.1f));
    }
    public void OnExitClick()
    {
        Application.Quit();
    }
    public void OnResumeClick()
    {
        Time.timeScale = 1;
        
    }
    public void OnRestartClick()
    {
     
        SceneManager.LoadScene("TowerDefence",LoadSceneMode.Single);
        Time.timeScale = 1f;
        StartCoroutine(Message(centerText,"",0.1f));
        
    }
    public void OnBuyLifeClick(int lifeCost)
    {


        if (gold >= lifeCost)
        {
            gold -= lifeCost;
            lives++;
            StartCoroutine(Message(GetComponentInParent<Text>(), " Lives +1", 3));
        }
        else
            StartCoroutine(Message(GetComponentInParent<Text>(), " Not Enough Gold!", 3));

    }
    public void OnBuyTower(Transform typePanel)
    {
        typePanel.gameObject.SetActive(!typePanel.gameObject.activeInHierarchy);
    }
    #endregion
    //------------------------------------------------------------------------------------
    //OWN METHODS

    /// <summary>
    /// Displays time to wave
    /// </summary>
    void WaveBreak()
    {
        if (wait && timeToWave >= 0)
        {
            timeToWave -= Time.deltaTime;
            if (FindObjectOfType<Audio>().audio.volume >= 0)
            {
                FindObjectOfType<Audio>().audio.volume -= 0.008f;
                if (FindObjectOfType<Audio>().audio.volume <= 0 && FindObjectOfType<Audio>().audio.isPlaying)
                {
                    FindObjectOfType<Audio>().audio.Stop();
                }
            }
            mobsLeft.text = "  " + (int)timeToWave + " sec left!";
            if (timeToWave < 0)
            {
                waveCount++;
                creepsOnWave = (int)(creepsOnWave * 1.2f);
                GameObject.FindObjectOfType<Spot>().nCreeps = 0;
                deadCreeps = 0;
              
                wait = false;
                timeToWave = constTime;
             
            }
        }
        if (wait&&timeToWave<0.2)
        {
            StartCoroutine(Message(centerText, "Wave "+waveCount));
        }
    }
    void LabelManager()
    {

        mobsLeft.text = "  Wave:Creeps count " + creepsOnWave + "\n  Creeps left: " + (creepsOnWave - deadCreeps);
        livesT.text = "Lives: " + lives + " \n  Gold:  " + gold + " ";
        randomText.text = "Time to random event: " + (int)randomTimer;

        if (lives <= 0&&!gameOver)
        {



            CenterPanel.gameObject.SetActive(true);
                StartCoroutine(Message(centerText, "Game over " + playerName + "!\nYou reached " + score + "pts", 50f));
                Time.timeScale = 0f;
                FindObjectOfType<Audio>().PlaySound(FindObjectOfType<Audio>().lose);
                gameOver = true;

        }
        if ((creepsOnWave - deadCreeps) <= 0 && !wait&&!gameOver)
        {
            
            StartCoroutine(Message(centerText, "You survived!",2f));

            wait = true;
        }
        
    }//Gold/life/creeps etc counters;
    /// <summary>
    /// 300 secs to random event, calls OnRandomClick()
    /// </summary>
    void RandomEvent()
    {


        if (randomTimer <= 0)
        {
            OnRandomClick();
            randomTimer = 300;

        }
        else randomTimer -= Time.deltaTime;
    }
    public void StatusBar()
    {
        if (target == null)
        {
            statusPanel.gameObject.SetActive(false);
        }
        Ray statusRay = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(statusRay, out statusHit))
        {
            if (Input.GetKey(KeyCode.LeftAlt)|| Input.GetKey(KeyCode.LeftControl)||Input.GetMouseButton(0))
            {

                if (statusHit.collider.gameObject.tag == "Buildings" || statusHit.collider.gameObject.tag == "Mobs" || statusHit.collider.gameObject.tag == "Boss")
                {                  
                    if (statusHit.collider.gameObject.tag == "Mobs"||statusHit.collider.gameObject.tag == "Boss")
                    {
                        statusPanel.gameObject.SetActive(true);

                        sell.gameObject.SetActive(false);
                        upgrade.gameObject.SetActive(false);
                        statusE.gameObject.SetActive(true);
                    }
                    if (statusHit.collider.gameObject.tag == "Buildings")
                    {
                        statusPanel.gameObject.SetActive(true);
                        sell.gameObject.SetActive(true);
                        upgrade.gameObject.SetActive(true);
                        statusE.gameObject.SetActive(false);
                    }
                    target = statusHit.collider.gameObject;
                }
            }
        }
        if (Input.GetButtonDown("Fire2"))
        {
            statusPanel.gameObject.SetActive(false);
            build.gameObject.SetActive(false);
            buyLife.gameObject.SetActive(false);
            random.gameObject.SetActive(false);
            restart.gameObject.SetActive(false);
            resume.gameObject.SetActive(false);
            options.gameObject.SetActive(false);
            SaveB.gameObject.SetActive(false);
            LoadB.gameObject.SetActive(false);
            exit.gameObject.SetActive(false);
            Content.parent.gameObject.SetActive(false);
            if (logPanel.parent.transform.position.y>0) logPanel.parent.transform.position = new Vector3(logPanel.parent.transform.position.x,- logPanel.parent.transform.position.y, logPanel.parent.transform.position.z);
        }
        if (target != null && (target.tag == "Buildings" || target.tag == "Mobs" || target.tag == "Boss"))
        {

            if (target.tag == "Buildings")
            {

                status.text = target.gameObject.GetComponent<fire>().info;
                upgrade.GetComponentInChildren<Text>().text = "Upgrade: " + target.GetComponent<fire>().level * price;
                sell.GetComponentInChildren<Text>().text = "Sell: " + price * target.GetComponent<fire>().level * 0.5;

            }
            else
            {
                status.text = target.gameObject.GetComponent<dest>().info;
                statusE.text = target.gameObject.GetComponent<dest>().effects;
            }
        }
    }
    public void Upgrade()
    {

        if (gold < price * target.GetComponent<fire>().level)
        {
            StartCoroutine(Message(GetComponentInParent<Text>(), " Not Enough Gold!", 3));
            return;
        }
        else gold -= price*target.GetComponent<fire>().level;
        target.GetComponent<Upgrade>().LevelUp();
    }
    public void Sell()
    {
        platformNameL.Add(target.name);
        platformsL.Add(target.GetComponentInParent<MeshRenderer>().gameObject);
        gold += (int)(price * target.GetComponent<fire>().level * 0.5);
        towerPosL.Remove(target.transform.position);
        towerTypesL.Remove(target.gameObject.name);
        FindObjectOfType<SerClass>().levels.Remove( target.GetComponent<fire>().level);
        Destroy(target.gameObject);
        StartCoroutine(Message(GetComponentInParent<Text>(), "OMG!Some idiot bought tower?\nOk,then!\nGold +" + 0.5 * price * target.GetComponent<fire>().level + "!", 3));

    }
    public void RandomSell() {
        if (GameObject.FindGameObjectsWithTag("Buildings").Length==0)
        {
            StartCoroutine(Message(GetComponentInParent<Text>(), "Nothing to steal :(", 3));
            return;
        }
        int rand = Random.Range(0, GameObject.FindGameObjectsWithTag("Buildings").Length);
        platformsL.Add(GameObject.FindGameObjectsWithTag("Buildings")[rand].GetComponentInParent<MeshRenderer>().gameObject);
        Destroy(GameObject.FindGameObjectsWithTag("Buildings")[rand]);
        StartCoroutine(Message(GetComponentInParent<Text>(), "Ur tower was stolen :(", 3));
    }
    /// <summary>
    /// Inverse state changing 
    /// </summary>
    public void SetActiveRec(params GameObject[] GO)
    {
        foreach (var item in GO)
        {
            item.SetActive(!item.gameObject.activeInHierarchy);

        }

    }
    public void SetActiveRec(GameObject go) { go.SetActive(!go.activeSelf); }
    public void DeleteNullFromList(List<GameObject> k)
    {

        for (int i = 0; i < k.Count; i++)
        {
            if (k[i] == null)
            {
                k.Remove(k[i]);
            }
        }
    }
    public void LayerChanger(int layer, List<GameObject> list)
    {
        foreach (var item in list)
        {
            item.layer = layer;
        }
    }
    public void Log(Transform panel)
    {
        panel.transform.position = new Vector3(panel.transform.position.x, -panel.transform.position.y, panel.transform.position.z);
    }
    /// <summary>
    /// For each GO in Resources/Prefabs/TowerTypes creates button with listener
    /// </summary>
    /// <param name="TowerType"></param>
    public void TowerTypeDownload(GameObject TowerType)
    {
        types = Resources.LoadAll<GameObject>("Prefabs/TowerTypes");
        for (int i = 0; i < types.Length; i++)
        {

            buf = (GameObject)Instantiate(TowerType, TowerType.transform.position, TowerType.transform.rotation);
            buf.transform.SetParent(Content);
            buf.name = types[i].name;
            buf.tag = "Updated";
            var index = i;
            buf.GetComponent<Button>().onClick.AddListener(() => StartCoroutine(TowerBuild(types[index].name, price)));

            buf.GetComponentInChildren<Text>().text = buf.name+": "+price;
            buf = null;


        }

    }
    /// <summary>
    /// Random build of random type of tower with messeges
    /// </summary>
    void RandomBuild()
    {
        int rand2 = Random.Range(0, types.Length);
        rand = Random.Range(0, platformsL.Count);
        if (platformsL.Count != 0)
        {
            buf = (GameObject)Instantiate(types[rand2], platformsL[rand].transform.position, platformsL[rand].transform.rotation);
            buf.GetComponent<fire>().towerType = Random.Range(0, 3);
            buf.transform.SetParent(platformsL[rand].transform);

            platformsL.RemoveAt(rand);

            StartCoroutine(Message(GetComponentInParent<Text>(), "Wow!Ur lucky!Free tower for ya!", 3));


            if (platformsL.Count == 0)
            {
                StartCoroutine(Message(GetComponentInParent<Text>(), " No place to bult!", 3));
            }

        }
        Debug.Log(types[rand2]);
    }

    //------------------------------------------------------------------------------------
    //IEnumerators block

    IEnumerator ColorChanger(Button random, Color startColor, Color finishColor, float duration)
    {
        bool end = false;
        float progress = 0;
        float increment = 0.5f / duration;


        while (true)
        {


            while (z.normalColor != finishColor && !end)
            {
                z.normalColor = Color.Lerp(startColor, finishColor, progress);
                z.highlightedColor = z.normalColor;
                random.colors = z;
                progress += increment;
                yield return new WaitForSeconds(0.10f);

            }
            end = true;

            while (z.normalColor != startColor && end)
            {

                z.normalColor = Color.Lerp(finishColor, startColor, 1 - progress);
                z.highlightedColor = z.normalColor;
                random.colors = z;
                progress -= increment;
                yield return new WaitForSeconds(0.10f);
            }
            end = false;
        }

    }
 public   IEnumerator Message(Text text, string message)
    {
        //TODO:redo this
        if (text == centerText)
        {
            CenterPanel.gameObject.SetActive(true);
        }
        text.text = message;
        yield return new WaitForSeconds(messageDelay);
        text.text = "";
        if (text == centerText)
        {
            CenterPanel.gameObject.SetActive(false);
        }

    }
   public IEnumerator Message(Text text, string message, float delay)
    {
        GameObject bufBlog;
        bufBlog = (GameObject)Instantiate(blog, option.position, option.rotation);
        bufBlog.transform.SetParent(option.transform);
        bufBlog.tag ="Untagged";
        bufBlog.GetComponent<Text>().text = message;
        if (text == centerText)
        {
            CenterPanel.gameObject.SetActive(true);
        }
        text.text =""+message;
        yield return new WaitForSeconds(delay);
        text.text ="";
        if (text == centerText)
        {
            CenterPanel.gameObject.SetActive(false);
        }

    }
  public  IEnumerator TowerBuild(string type, int towerCost)
    {


        if (gold < towerCost)
        {
            StartCoroutine(Message(GetComponentInParent<Text>(), " Not Enough Gold!", 3));

            yield break;
        }
        else
        {
            gold -= towerCost;

        }
        chosePlatform = true;
        if (chosePlatform)
        {
            foreach (var item in platformsL)
            {

                item.GetComponent<MeshRenderer>().material = greenmat;
            }

        }
        foreach (var item in build.GetComponentsInChildren<Button>())
        {

            if (item.transform.parent == build.transform)
            {
                item.gameObject.SetActive(true);
                Debug.Log(item.name);

            }

        }
        while (true)
        {

            if (Input.GetButtonDown("Fire1"))
            {
                DeleteNullFromList(towers);

                if (chosePlatform)
                {

                    Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                    LayerChanger(2, towers);
                    if (Physics.Raycast(ray, out buildHit))
                    {
                        if (buildHit.collider.tag == "Platforms")
                        {
                            buildHit.collider.gameObject.GetComponent<MeshRenderer>().material = defmat;

                            platformsL.Remove(buildHit.collider.gameObject);
                            chosePlatform = false;
                            buf = (GameObject)Instantiate(Resources.Load("Prefabs/TowerTypes/" + type), buildHit.collider.gameObject.transform.position, buildHit.collider.gameObject.transform.rotation);

                            buf.transform.SetParent(buildHit.transform);

                            towers.Add(buf);
                            //Serialization lists
                            parent.Add(buildHit.collider.gameObject.name);
                           towerPosL.Add( buildHit.transform.position);
                            towerTypesL.Add(type);
                            platformNameL.Remove(buildHit.collider.gameObject.name);
                            //--------
                            StartCoroutine(Message(GetComponentInParent<Text>(), "Success", 3));


                        }

                    }

                    foreach (var item in platformsL)
                    {
                        item.GetComponent<MeshRenderer>().material = defmat;
                    }

                    LayerChanger(0, towers);

                }
                break;
            }

            yield return null;
        }

    }
    /// <summary>
    /// Every 0.5 sec changes var
    /// </summary>
    /// <returns></returns>
    IEnumerator RandPrice() {
        while (true)
        {
            price = (int)Random.Range(1, 500);
           yield return new WaitForSeconds(0.5f);

         
           
        }
    
    }
    void OnSafeClick()
    {
        StartCoroutine(Message(centerText,"Succesfully saved!",1f));
    }
}
