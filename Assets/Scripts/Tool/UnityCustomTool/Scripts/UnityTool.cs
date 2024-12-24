using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Unity.CustomTool
{
    public static class UnityTool
    {
        /// <summary>
        /// Find object in scene.
        /// </summary>
        /// <param name="GameObjectName">GameObject name that you want to find.</param>
        /// <returns></returns>
        public static GameObject FindGameObject(string GameObjectName)
        {
            GameObject pTmpGameObj = GameObject.Find(GameObjectName);
            if (pTmpGameObj == null)
            {
                Debug.LogWarning("場景中找不到GameObject[" + GameObjectName + "]物件");

                return null;
            }
            return pTmpGameObj;
        }

        /// <summary>
        /// Find child object in container.
        /// </summary>
        /// <param name="Container">Parent object of try to find gameObject.</param>
        /// <param name="gameObjectName">The object you want to find.</param>
        /// <returns></returns>
        public static GameObject FindChildGameObject(GameObject Container, string gameObjectName)
        {
            if (Container == null)
            {
                Debug.LogError("NGUICustomTools.GetChild:Container - null");

                return null;
            }

            Transform pGameObjectTF = null;

            //Is container object itself or not.
            if (Container.name == gameObjectName)
            {
                pGameObjectTF = Container.transform;
            }
            else
            {
                Transform[] allChildren = Container.transform.GetComponentsInChildren<Transform>();
                foreach (Transform child in allChildren)
                {
                    if (child.name == gameObjectName)
                    {
                        if (pGameObjectTF == null)
                            pGameObjectTF = child;
                        else
                            Debug.LogWarning("Container[" + Container.name + "[下找出重複的物件名稱[" + gameObjectName + "]");
                    }
                }
            }

            //Find nothing.
            if (pGameObjectTF == null)
            {
                Debug.Log("物件[" + Container.name + "]找不到子物件[" + gameObjectName + "]");

                return null;
            }

            return pGameObjectTF.gameObject;
        }
    }

    public static class UITool
    {
        private static GameObject m_CanvasObj = null; //2D canvas in scene.

        /// <summary>
        /// Find UI in Canvas.
        /// </summary>
        /// <param name="UIName"></param>
        /// <returns></returns>
        public static GameObject FindUIGameObject(string UIName)
        {
            if (m_CanvasObj == null)
                m_CanvasObj = UnityTool.FindGameObject("Canvas");
            if (m_CanvasObj == null)
                return null;
            return UnityTool.FindChildGameObject(m_CanvasObj, UIName);
        }

        /// <summary>
        /// 尋找特定Canvas下的UI物件
        /// </summary>
        /// <param name="canvasName">要尋找的UI物件所在的Canvas</param>
        /// <param name="uiName">要尋找的UI物件</param>
        /// <returns></returns>
        public static GameObject FindGameObjectInSpecificCanvas(string canvasName, string uiName)
        {
            GameObject currentCanvasObj = UnityTool.FindGameObject(canvasName);
            if (currentCanvasObj == null)
                return null;
            return UnityTool.FindChildGameObject(currentCanvasObj, uiName);
        }

        /// <summary>
        /// Get UI component.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="Container">Parent object of try to find object.</param>
        /// <param name="UIName">The name of UI object.</param>
        /// <returns>Return component you want to find.</returns>
        public static T GetUIComponent<T>(GameObject Container, string UIName) where T : UnityEngine.Component
        {
            //Find child gameObjet;
            GameObject ChildGameObject = UnityTool.FindChildGameObject(Container, UIName);
            if (ChildGameObject == null)
                return null;

            T tempObj = ChildGameObject.GetComponent<T>();
            if (tempObj == null)
            {
                Debug.LogWarning("元件[" + UIName + "]不是[" + typeof(T) + "]");
                return null;
            }
            return tempObj;
        }
    }
}
