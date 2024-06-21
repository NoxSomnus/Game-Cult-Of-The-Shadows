using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

public static class SaveManager 
{
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

}
