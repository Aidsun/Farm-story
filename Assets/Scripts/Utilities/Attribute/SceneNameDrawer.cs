using UnityEditor;
using UnityEngine;
//场景名选择样式列表
//这个代码定义inspector里选择场景名时的样式
[CustomPropertyDrawer(typeof(SceneNameAttribute))]
public class SceneNameDrawer : PropertyDrawer
{
    //场景序号（默认为-1）
    int sceneIndex = -1;
    //场景名的GUI数组
    GUIContent[] sceneNames;
    //字符串需要切割的字符
    readonly string[] scenePathSplit = { "/", ".unity" };
    public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
    {
        if (EditorBuildSettings.scenes.Length == 0) return;
        if (sceneIndex == -1)
        {
            GetSceneNameArray(property);
        }

        int oldIndex = sceneIndex;

        sceneIndex = EditorGUI.Popup(position, label, sceneIndex, sceneNames);

        if(oldIndex != sceneIndex) 
        {
             property.stringValue = sceneNames[sceneIndex].text;
        }

    }

    //切割场景文件名字，获的场景名
    private void GetSceneNameArray(SerializedProperty property)
    {
        var scenes = EditorBuildSettings.scenes;
        //初始化数组
        sceneNames = new GUIContent[scenes.Length];

        for (int i = 0; i < sceneNames.Length; i++) 
        {
            string path = scenes[i].path;
            string[] splitPath = path.Split(scenePathSplit,System.StringSplitOptions.RemoveEmptyEntries);

            string sceneName = "";
            if (splitPath.Length > 0) 
            {
                sceneName = splitPath[splitPath.Length - 1];
            }
            else
            {
                sceneName = "场景已移除";
            }
            sceneNames[i] = new GUIContent(sceneName);
        }
        if(sceneNames.Length == 0)
        {
            sceneNames = new[] { new GUIContent("请检查你的Build Settings") };
        }
        //如果场景名为空
        if (!string.IsNullOrEmpty(property.stringValue))
        {
            bool nameFound = false;
            for(int i = 0; i < sceneNames.Length; i++)
            {
                if (sceneNames[i].text == property.stringValue)
                {
                    sceneIndex = i;
                    nameFound = true;
                    break;
                }
            }
            if(nameFound == false)
            {
                sceneIndex = 0;
            }
        }
        else
        {
            sceneIndex = 0;
        }

        property.stringValue = sceneNames[sceneIndex].text;

    }
} 