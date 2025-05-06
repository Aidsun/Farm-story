using UnityEditor;
using UnityEngine;
//������ѡ����ʽ�б�
//������붨��inspector��ѡ�񳡾���ʱ����ʽ
[CustomPropertyDrawer(typeof(SceneNameAttribute))]
public class SceneNameDrawer : PropertyDrawer
{
    //������ţ�Ĭ��Ϊ-1��
    int sceneIndex = -1;
    //��������GUI����
    GUIContent[] sceneNames;
    //�ַ�����Ҫ�и���ַ�
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

    //�и���ļ����֣���ĳ�����
    private void GetSceneNameArray(SerializedProperty property)
    {
        var scenes = EditorBuildSettings.scenes;
        //��ʼ������
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
                sceneName = "�������Ƴ�";
            }
            sceneNames[i] = new GUIContent(sceneName);
        }
        if(sceneNames.Length == 0)
        {
            sceneNames = new[] { new GUIContent("�������Build Settings") };
        }
        //���������Ϊ��
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