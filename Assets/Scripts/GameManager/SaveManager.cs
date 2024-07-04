using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Collections.Generic;

public static class SaveManager 
{
    #region DataPlayer
    public static void SavePlayerData(Parameters pPlayer)
    {
        PlayerData playerData = new PlayerData(pPlayer);
        string dataPATH = Application.persistentDataPath + "/playerdata.save";
        FileStream fileStream = new FileStream(dataPATH, FileMode.Create);
        BinaryFormatter binaryFormatter = new BinaryFormatter();
        binaryFormatter.Serialize(fileStream, playerData);
        fileStream.Close();
        Debug.Log("guadao");
    }
    public static void OnlySavePlayerData(PlayerData pd)
    {
        PlayerData playerData = pd;
        string dataPATH = Application.persistentDataPath + "/playerdata.save";
        FileStream fileStream = new FileStream(dataPATH, FileMode.Create);
        BinaryFormatter binaryFormatter = new BinaryFormatter();
        binaryFormatter.Serialize(fileStream, playerData);
        fileStream.Close();
    }

    public static PlayerData LoadPlayerData()
    {
        
        string dataPATH = Application.persistentDataPath + "/playerdata.save";
        if (File.Exists(dataPATH))
        {
            FileStream fileStream = new FileStream(dataPATH, FileMode.Open);
            BinaryFormatter binaryFormatter = new BinaryFormatter();    
            PlayerData playerData = (PlayerData) binaryFormatter.Deserialize(fileStream);
            fileStream.Close();
            return playerData;
        }
        else
        {
            Debug.LogError("no hay nada guardado papu");
            return null;
        }
    }
    public static void DeletePlayerData()
    {
        string dataPATH = Application.persistentDataPath + "/playerdata.save";
        if (File.Exists(dataPATH))
        {
            File.Delete(dataPATH);
            Debug.Log("Datos del jugador eliminados");
        }
        else
        {
            Debug.LogError("No hay datos del jugador guardados para eliminar");
        }
    }
    #endregion

    #region SceneData

    public static void SaveObjectData(List<SceneObjectData> sceneobjectData)
    {

        // Guardar la lista actualizada en el archivo
        string dataPATH = Application.persistentDataPath + "/SceneObjectData.save";
        FileStream fileStream = new FileStream(dataPATH, FileMode.Create);
        BinaryFormatter binaryFormatter = new BinaryFormatter();
        binaryFormatter.Serialize(fileStream, sceneobjectData);
        fileStream.Close();

        Debug.Log("Objeto guardado");
    }

    public static List<SceneObjectData> LoadSceneObjectData()
    {
        string dataPATH = Application.persistentDataPath + "/SceneObjectData.save";
        if (File.Exists(dataPATH))
        {
            FileStream fileStream = new FileStream(dataPATH, FileMode.Open);
            BinaryFormatter binaryFormatter = new BinaryFormatter();
            List<SceneObjectData> sceneObjectDataList = (List<SceneObjectData>)binaryFormatter.Deserialize(fileStream);
            fileStream.Close();
            return sceneObjectDataList;
        }
        else
        {
            return new List<SceneObjectData>();
        }
    }

    public static List<SceneObjectData> updateSceneData(List<SceneObjectData> objectData)
    {
        // Cargar los datos existentes del archivo
        List<SceneObjectData> sceneObjectDataList = LoadSceneObjectData();

        foreach (SceneObjectData savedSceneObject in sceneObjectDataList)
        {
            foreach (SceneObjectData newSceneObjectData in objectData)
            {
                if (savedSceneObject.sceneId  == newSceneObjectData.sceneId && 
                    savedSceneObject.name     == newSceneObjectData.name && 
                    savedSceneObject.objectID == newSceneObjectData.objectID)
                {
                    savedSceneObject.enable = newSceneObjectData.enable;
                }
            }
        }
        return sceneObjectDataList;

    }

    #endregion
}
