using UnityEngine;
using System.Collections;
using System;
using System.IO;
using System.Xml;

public class MapCamera : MonoBehaviour {

	/// <summary>  
	/// 鼠标射线  
	/// </summary>  
	private Ray m_ray;
	/// <summary>  
	/// 射线碰撞的结构  
	/// </summary>  
	private RaycastHit2D m_rayhit;
	/// <summary>
	/// 点击到的物体
	/// </summary>
	private GameObject m_clickItem;
	/// <summary>
	/// 读取到的物体
	/// </summary>
	private GameObject m_readItem;
	/// <summary>
	/// 地图
	/// </summary>
	public GameObject m_Map;
	/// <summary>
	/// 地图元素
	/// </summary>
	public ArrayList m_Items;
	
	public GameObject m_ItemWall;
	public GameObject m_ItemSturdyRobot;
	public GameObject m_ItemSteel;
	public GameObject m_ItemFastRobot;
	public GameObject m_ItemStupidRobot;
	/// <summary>
	/// path of map_<level>.xml
	/// </summary>
	private string m_sXmlPath;
	/// <summary>
	/// XML数据
	/// </summary>
	private string m_sMyMoveSpeed;      //我的移动速度
	private string m_sMyLife;           //我的生命
	private string m_currentLevel;
	private string m_readLevel;
	private string m_toolRate;
	private string m_visibility;
	void Start()
	{
		m_Items = new ArrayList();
		m_clickItem = null;
		m_readItem = null;

		m_sMyMoveSpeed = "1";
		m_sMyLife = "3";
		m_currentLevel = "1";
		m_toolRate = "0.5";
		m_visibility = "1";
		m_readLevel = "1";
		m_sXmlPath = Application.dataPath + "/Map/map_1.xml";
	}
	
	void Update()
	{
		//检测鼠标左键的拾取  
		if (Input.GetMouseButtonDown(0))
		{
			//鼠标的屏幕坐标空间位置转射线  
			m_ray = Camera.main.ScreenPointToRay(Input.mousePosition);
			m_rayhit = Physics2D.GetRayIntersection(m_ray);
			//射线检测，相关检测信息保存到RaycastHit 结构中  
			if (m_rayhit)
			{
				//打印射线碰撞到的对象的名称  
				Debug.Log(m_rayhit.collider.gameObject.name);
				if (m_rayhit.collider.gameObject.name == "walls" ||
				    m_rayhit.collider.gameObject.name == "sturdyRobots" ||
				    m_rayhit.collider.gameObject.name == "steels" ||
				    m_rayhit.collider.gameObject.name == "fastRobots"||
				    m_rayhit.collider.gameObject.name == "stupidRobots")
				{
					m_clickItem = Instantiate(m_rayhit.collider.gameObject, m_rayhit.collider.transform.position, Quaternion.identity) as GameObject;
					m_clickItem.name = m_rayhit.collider.gameObject.name.Substring(0, m_rayhit.collider.gameObject.name.Length - 1);
					//m_clickItem.tag = m_clickItem.name; //ERROR: not define tag in editor

				}
				else if (m_rayhit.collider.gameObject.name == "wall" ||
				         m_rayhit.collider.gameObject.name == "sturdyRobot" ||
				         m_rayhit.collider.gameObject.name == "steel" ||
				         m_rayhit.collider.gameObject.name == "fastRobot"||
				         m_rayhit.collider.gameObject.name == "stupidRobot")
				{
					m_clickItem = m_rayhit.collider.gameObject;
				}
				else
				{
					m_clickItem = null;
				}
			}
			else
			{
				m_clickItem = null;
			}
		}
		if (Input.GetMouseButton(0))
		{
			if (m_clickItem != null)
			{
				//Debug.Log("haha"+camera.ScreenToWorldPoint(Input.mousePosition).x);
				m_clickItem.transform.position = SetPointInMap(new Vector3(camera.ScreenToWorldPoint(Input.mousePosition).x, camera.ScreenToWorldPoint(Input.mousePosition).y, -1));

			}
		}
		if (Input.GetMouseButtonUp(0))
		{
			if (m_clickItem != null)
			{
				Vector3 cur_point = SetPointInMap(new Vector3(camera.ScreenToWorldPoint(Input.mousePosition).x, camera.ScreenToWorldPoint(Input.mousePosition).y, -1));
				if (m_Map.collider2D.OverlapPoint(cur_point))
				{
					m_clickItem.transform.position = cur_point;
				}
				else
				{
					Destroy(m_clickItem);
				}
			}
		}
	}
	
	Vector3 SetPointInMap(Vector3 cur_point)
	{
		Debug.Log("current x is "+ cur_point[2]);
		Vector3 v_temp = cur_point / 1.0f;
		v_temp = new Vector3(Convert.ToInt32(v_temp.x), Convert.ToInt32(v_temp.y), Convert.ToInt32(v_temp.z));
		Debug.Log("final x is "+ v_temp[0]);
		return v_temp * 1.0f;

	}
	
	void OnGUI()
	{
		GUI.Label(new Rect(10, 10, 100, 30), "Current Level:");
		m_currentLevel = GUI.TextField(new Rect(110, 10, 50, 20), m_currentLevel);
		GUI.Label(new Rect(10, 40, 100, 30), "Player Speed:");
		m_sMyMoveSpeed = GUI.TextField(new Rect(110, 40, 50, 20), m_sMyMoveSpeed);
		GUI.Label(new Rect(10, 70, 100, 30), "Player HP:");
		m_sMyLife = GUI.TextField(new Rect(110, 70, 50, 20), m_sMyLife);
		GUI.Label(new Rect(10, 100, 100, 30), "Tool Rate:");
		m_toolRate = GUI.TextField(new Rect(110, 100, 50, 20), m_toolRate);
		GUI.Label(new Rect(10, 130, 100, 30), "Visibility Rate:");
		m_visibility = GUI.TextField(new Rect(110, 130, 50, 20), m_visibility);

		m_readLevel = GUI.TextField(new Rect(110, 220, 50, 20), m_readLevel);

		if (GUI.Button(new Rect(10, 180, 80, 30), "保存"))
		{
			m_sXmlPath = Application.dataPath + "/Map/map_"+m_currentLevel+".xml";
			SaveInXml();
		}
		if (GUI.Button(new Rect(10, 220, 80, 30), "读取"))
		{

			m_sXmlPath = Application.dataPath + "/Map/map_"+m_readLevel+".xml";
			ReadFromXml();
		}
		if (GUI.Button(new Rect(10, 260, 80, 30), "返回"))
		{
			Application.LoadLevel("StartScene");
		}
	}
	
	bool SaveInXml()
	{
		m_sXmlPath = Application.dataPath + "/Map/map_"+m_currentLevel+".xml";
		if (File.Exists(m_sXmlPath))
		{
			File.Delete(m_sXmlPath);
		}
		XmlDocument xmlDoc = new XmlDocument();
		XmlElement data = xmlDoc.CreateElement("data");
		xmlDoc.AppendChild(data);
		//配置
		XmlElement xml_config = xmlDoc.CreateElement("config");
		data.AppendChild(xml_config);
		//玩家移动速度
		XmlElement xml_my_move_speed = xmlDoc.CreateElement("PlayerMoveSpeed");
		xml_my_move_speed.InnerText = m_sMyMoveSpeed;
		xml_config.AppendChild(xml_my_move_speed);
		//玩家生命
		XmlElement xml_my_life = xmlDoc.CreateElement("PlayerLife");
		xml_my_life.InnerText = m_sMyLife;
		xml_config.AppendChild(xml_my_life);
		//tool rate
		XmlElement xml_tool_rate = xmlDoc.CreateElement("ToolRate");
		xml_tool_rate.InnerText = m_toolRate;
		xml_config.AppendChild(xml_tool_rate);
		//visibility rate
		XmlElement xml_visibility_rate = xmlDoc.CreateElement("VisibilityRate");
		xml_visibility_rate.InnerText = m_visibility;
		xml_config.AppendChild(xml_visibility_rate);

		//地图数据
		XmlElement xml_items = xmlDoc.CreateElement("items");
		data.AppendChild(xml_items);
		SaveObjects ();
		foreach (GameObject item in m_Items)
		{
			Debug.Log("要保存的数据：" + item.name + "位置：" + item.transform.position.x + "," + item.transform.position.y);
			
			//save in xml file
			XmlElement xml_item = xmlDoc.CreateElement("item");
			xml_items.AppendChild(xml_item);
			XmlElement name = xmlDoc.CreateElement("name");
			name.InnerText = item.name;
			xml_item.AppendChild(name);
			
			XmlElement position_x = xmlDoc.CreateElement("position_x");
			position_x.InnerText = item.transform.position.x.ToString();
			xml_item.AppendChild(position_x);
			
			XmlElement position_y = xmlDoc.CreateElement("position_y");
			position_y.InnerText = item.transform.position.y.ToString();
			xml_item.AppendChild(position_y);
		}
		xmlDoc.Save(m_sXmlPath);
		Debug.Log("创建XML完毕");
		m_Items.Clear();
		Application.LoadLevel(Application.loadedLevel);
		return true;
	}
	
	void ReadFromXml()
	{
		if (File.Exists(m_sXmlPath))
		{
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
			
			Vector3 psition = new Vector3(float.Parse(position_x.InnerText), float.Parse(position_y.InnerText), -1);
			
			if (current_name.InnerText == "wall")
			{
				m_readItem = Instantiate(m_ItemWall, psition, Quaternion.identity) as GameObject;
			}
			
			else if (current_name.InnerText == "sturdyRobot")
			{
				m_readItem = Instantiate(m_ItemSturdyRobot, psition, Quaternion.identity) as GameObject;
			}
			
			else if (current_name.InnerText == "steel")
			{
				m_readItem = Instantiate(m_ItemSteel, psition, Quaternion.identity) as GameObject;
			}
			
			else if (current_name.InnerText == "fastRobot")
			{
				m_readItem = Instantiate(m_ItemFastRobot, psition, Quaternion.identity) as GameObject;
			}

			else if (current_name.InnerText == "stupidRobot")
			{
				m_readItem = Instantiate(m_ItemStupidRobot, psition, Quaternion.identity) as GameObject;
			}

			else continue;
			
			m_readItem.name = current_name.InnerText;
			
			Debug.Log("要读取的数据：" + m_readItem.name + "位置：" + m_readItem.transform.position.x + "," + m_readItem.transform.position.y);
			
			//m_Items.Add(m_readItem);
		}
	}

	void CleanObjects()
	{
		object[] gameObjects = GameObject.FindSceneObjectsOfType(typeof(Transform)) as object[];
		foreach(Transform tempObjects in gameObjects)
		{
			if(tempObjects.transform!=null)
			{
				if(tempObjects.name=="wall"||tempObjects.name=="steel"||tempObjects.name=="fastRobot"||tempObjects.name=="sturdyRobot"||tempObjects.name=="stupidRobot")
				{
					Destroy(tempObjects.gameObject);
				}
			}
		}
	}

	void SaveObjects()
	{
		object[] gameObjects = GameObject.FindSceneObjectsOfType(typeof(Transform)) as object[];
		foreach(Transform tempObjects in gameObjects)
		{
			if(tempObjects.transform!=null)
			{
				if(tempObjects.name=="wall"||tempObjects.name=="steel"||tempObjects.name=="fastRobot"||tempObjects.name=="sturdyRobot"||tempObjects.name=="stupidRobot")
				{
					m_Items.Add(tempObjects.gameObject);
				}
			}
		}
	}

}
