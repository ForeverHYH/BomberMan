using UnityEngine;
using System.Collections;
using System;
using System.IO;
using System.Xml;

public class SenceLoad : MonoBehaviour {
	private string m_sXmlPath;
	public ArrayList m_Items;
	private GameObject m_readItem;

	private string m_sMyMoveSpeed;      //我的移动速度
	private string m_sMyLife;           //我的生命
	private string m_currentLevel;
	private string m_readLevel;
	public string m_toolRate;
	private string m_visibility;

	public GameObject m_ItemWall;
	public GameObject m_ItemSturdyRobot;
	public GameObject m_ItemSteel;
	public GameObject m_ItemFastRobot;
	public GameObject m_ItemStupidRobot;



	public ArrayList CreatureList = new ArrayList();
	public ArrayList HinderList = new ArrayList();



	void Awak(){

	}
	// Use this for initialization
	void Start () {
		Time.timeScale = 1;

		m_Items = new ArrayList();
		m_readItem = null;

		//m_readLevel = StaticComponents.CURRENT_LEVEL.ToString ();
		m_readLevel = StaticComponents.CURRENT_LEVEL.ToString ();
		m_sXmlPath = Application.dataPath + "/Map/map_"+m_readLevel+".xml";
		Debug.Log (m_sXmlPath);
		ReadFromXml (m_sXmlPath);

	}
	
	// Update is called once per frame
	void Update () {

		//GameObject.Find ("Ground cube").SetActive (false);
		//GameObject.Find ("CanonBorn").SetActive (false);
	}

	void ReadFromXml(string xmlPath)
	{
		if (File.Exists(xmlPath))
		{
			//Debug.Log("Read Start!");
			m_Items.Clear();
			CleanObjects();
			m_readItem = null;
			XmlDocument xmlDoc = new XmlDocument();
			xmlDoc.Load(m_sXmlPath);
			//读取配置
			XmlNode xml_config = xmlDoc.SelectSingleNode("data/config");
			ReadConfig(xml_config);
			//读取地图
			XmlNode xml_items = xmlDoc.SelectSingleNode("data/items");
			ReadMap(xml_items);
		}
		else{
			Debug.Log("Cannot find path");
		}
	}
	
	void ReadConfig(XmlNode xml_config)
	{
		XmlNode xml_my_move_speed = xml_config.SelectSingleNode("PlayerMoveSpeed");
		m_sMyMoveSpeed = xml_my_move_speed.InnerText;
		
		XmlNode xml_my_life = xml_config.SelectSingleNode("PlayerLife");
		m_sMyLife = xml_my_life.InnerText;
		
		XmlNode xml_tool_rate = xml_config.SelectSingleNode("ToolRate");
		m_toolRate = xml_tool_rate.InnerText;
		
		XmlNode xml_visibility_rate = xml_config.SelectSingleNode("VisibilityRate");
		m_visibility = xml_visibility_rate.InnerText;
	}
	
	void ReadMap(XmlNode xml_items)
	{
		CleanObjects ();
		XmlNodeList items = xml_items.SelectNodes("item");
		foreach (XmlNode current_node in items)
		{
			XmlNode current_name = current_node.SelectSingleNode("name");
			XmlNode position_x = current_node.SelectSingleNode("position_x");
			XmlNode position_y = current_node.SelectSingleNode("position_y");
			
			Vector3 psition = new Vector3(float.Parse(position_x.InnerText), 1, float.Parse(position_y.InnerText));
			Vector3 robotPosition = new Vector3(float.Parse(position_x.InnerText), 0.6f, float.Parse(position_y.InnerText));
			if (current_name.InnerText == "wall")
			{
				m_readItem = Instantiate(m_ItemWall, psition, Quaternion.identity) as GameObject;
			}
			
			else if (current_name.InnerText == "sturdyRobot")
			{
				m_readItem = Instantiate(m_ItemSturdyRobot, robotPosition, Quaternion.identity) as GameObject;
				CreatureList.Add (m_readItem.transform);
			}
			
			else if (current_name.InnerText == "steel")
			{
				m_readItem = Instantiate(m_ItemSteel, psition, Quaternion.identity) as GameObject;
				HinderList.Add(m_readItem.transform);
			}
			
			else if (current_name.InnerText == "fastRobot")
			{
				m_readItem = Instantiate(m_ItemFastRobot, robotPosition, Quaternion.identity) as GameObject;
				CreatureList.Add (m_readItem.transform);
			}

			else if (current_name.InnerText == "stupidRobot")
			{
				m_readItem = Instantiate(m_ItemStupidRobot, robotPosition, Quaternion.identity) as GameObject;
				CreatureList.Add (m_readItem.transform);
			}
			else continue;
			
			m_readItem.name = current_name.InnerText;
			
			//Debug.Log("要读取的数据：" + m_readItem.name + "位置：" + m_readItem.transform.position.x + "," + m_readItem.transform.position.y);
			
			//m_Items.Add(m_readItem);
		}
	}
	void CleanObjects()
	{
		object[] gameObjects = GameObject.FindObjectsOfType(typeof(Transform)) as object[];
		foreach(Transform tempObjects in gameObjects)
		{
			if(tempObjects.transform!=null)
			{
				if(tempObjects.name=="wall"||tempObjects.name=="steel"||tempObjects.name=="fastRobot"||tempObjects.name=="sturdyRobot")
				{
					Destroy(tempObjects.gameObject);
				}
			}
		}
	}
}
