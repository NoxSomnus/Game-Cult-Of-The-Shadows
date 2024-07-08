using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Collections.Generic;

public static class SaveManager 
{
    #region DataPlayer
    public static void SavePlayerData(GameSaveData data)
    {
        //PlayerData playerData = new PlayerData(pPlayer, sceneId);
        string dataPATH = Application.persistentDataPath + "/GameData.save";
        FileStream fileStream = new FileStream(dataPATH, FileMode.Create);

        BinaryFormatter binaryFormatter = new BinaryFormatter();
        binaryFormatter.Serialize(fileStream, data);
        fileStream.Close();
        Debug.Log("guadao");
    }
    public static void OnlySavePlayerData(GameSaveData gsd)
    {
        GameSaveData gameData = gsd;
        string dataPATH = Application.persistentDataPath + "/GameData.save";
        FileStream fileStream = new FileStream(dataPATH, FileMode.Create);
        BinaryFormatter binaryFormatter = new BinaryFormatter();
        binaryFormatter.Serialize(fileStream, gameData);
        fileStream.Close();
    }

    public static GameSaveData LoadPlayerData(Parameters parameters)
    {
        
        string dataPATH = Application.persistentDataPath + "/GameData.save";
        if (File.Exists(dataPATH))
        {
            FileStream fileStream = new FileStream(dataPATH, FileMode.Open);
            BinaryFormatter binaryFormatter = new BinaryFormatter();    
            GameSaveData data = (GameSaveData) binaryFormatter.Deserialize(fileStream);
            fileStream.Close();
            return data;
        }
        else
        {
            GameSaveData data = new GameSaveData(parameters);
            Debug.LogError("no hay nada guardado papu");
            return data;
        }
    }
    public static void DeletePlayerData()
    {
        string dataPATH = Application.persistentDataPath + "/GameData.save";
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

    

    #endregion
}
