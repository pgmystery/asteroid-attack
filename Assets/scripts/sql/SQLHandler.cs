using UnityEngine;
using UnityEngine.Networking;
using System.Data;
using Mono.Data.Sqlite;
using System.IO;
using System.Collections;
using System;

// https://medium.com/@rizasif92/sqlite-and-unity-how-to-do-it-right-31991712190

public class SQLHandler : MonoBehaviour
{
    public static SQLHandler sqlHandler;

    // public static IDataReader RunCommand(string cmd);

    private IDbConnection dbcon;
    private IDbCommand dbcmd;
    private IDataReader reader;

    void Awake() {
        if (sqlHandler == null) {
            sqlHandler = this;
        }
        else if (sqlHandler != this) {
            Destroy(gameObject);
        }

        // LoadDatabase();
        StartCoroutine(LoadDatabase());
    }

    // /storage/emulated/0/Android/data/com.Company.asteroid_attack/files
    IEnumerator LoadDatabase() {
    // void LoadDatabase() {
        // Debug.Log(Application.persistentDataPath + "/asteroid_attack.db");
        if (!File.Exists(Application.persistentDataPath + "/asteroid_attack.db")) {
            string databasePath = Path.Combine(Application.streamingAssetsPath + "/", "asteroid_attack.db");
            if (Application.platform == RuntimePlatform.Android) {
                // WWW db = new WWW(Application.streamingAssetsPath + "/asteroid_attack.db");
                // File.WriteAllBytes(Application.persistentDataPath + "/asteroid_attack.db", db.bytes);
                UnityWebRequest www = UnityWebRequest.Get(databasePath);
                yield return www.SendWebRequest();
                if (www.isNetworkError || www.isHttpError) {
                    Debug.LogError(www.error);
                }
                else {
                    File.WriteAllBytes(Application.persistentDataPath + "/asteroid_attack.db", www.downloadHandler.data);
                }
            }
            else {
                File.Copy(databasePath, Application.persistentDataPath + "/asteroid_attack.db");
            }
        }
        // Debug.Log(Path.Combine(Application.streamingAssetsPath + "/", "asteroid_attack.db"));
        Debug.Log(Application.persistentDataPath + "/asteroid_attack.db");
        if (File.Exists(Application.persistentDataPath + "/asteroid_attack.db")) {
            string connection = "URI=file:" + Application.persistentDataPath + "/asteroid_attack.db";
            dbcon = new SqliteConnection(connection);
        }
    }

    public DataTable RunCommand(string cmd) {
        dbcon.Open();
        dbcmd = dbcon.CreateCommand();
        dbcmd.CommandText = cmd;
        reader = dbcmd.ExecuteReader();
        // DataTable dt = new DataTable();
        // dt.Load(reader);  // NOT WORKING ON ANDROID!!!
        DataTable dt = CreateDataTable(reader);

        dbcon.Close();
        return dt;

        // RunSQLCommand sqlCommand = new RunSQLCommand(dbcon, dbcmd);
        // return sqlCommand.RunCommand(cmd);
    }

    private static DataTable CreateDataTable(IDataReader reader) {
        DataTable dt = new DataTable();

        // Add all the columns.
        for (int i = 0; i < reader.FieldCount; i++)
        {
            DataColumn col = new DataColumn();
            col.DataType = reader.GetFieldType(i);
            col.ColumnName = reader.GetName(i);
            dt.Columns.Add(col);
        }

        while (reader.Read())
        {
            DataRow row = dt.NewRow();
            for (int i = 0; i < reader.FieldCount; i++)
            {
            // Ignore Null fields.
            if (reader.IsDBNull(i)) continue;

            if (reader.GetFieldType(i) == typeof(String))
            {
                row[dt.Columns[i].ColumnName] = reader.GetString(i);
            }
            else if (reader.GetFieldType(i) == typeof(Int16))
            {
                row[dt.Columns[i].ColumnName] = reader.GetInt16(i);
            }
            else if (reader.GetFieldType(i) == typeof(Int32))
            {
                row[dt.Columns[i].ColumnName] = reader.GetInt32(i);
            }
            else if (reader.GetFieldType(i) == typeof(Int64))
            {
                row[dt.Columns[i].ColumnName] = reader.GetInt64(i);
            }
            else if (reader.GetFieldType(i) == typeof(Boolean))
            {
                row[dt.Columns[i].ColumnName] = reader.GetBoolean(i); ;
            }
            else if (reader.GetFieldType(i) == typeof(Byte))
            {
                row[dt.Columns[i].ColumnName] = reader.GetByte(i);
            }
            else if (reader.GetFieldType(i) == typeof(Char))
            {
                row[dt.Columns[i].ColumnName] = reader.GetChar(i);
            }
            else if (reader.GetFieldType(i) == typeof(DateTime))
            {
                row[dt.Columns[i].ColumnName] = reader.GetDateTime(i);
            }
            else if (reader.GetFieldType(i) == typeof(Decimal))
            {
                row[dt.Columns[i].ColumnName] = reader.GetDecimal(i);
            }
            else if (reader.GetFieldType(i) == typeof(Double))
            {
                row[dt.Columns[i].ColumnName] = reader.GetDouble(i);
            }
            else if (reader.GetFieldType(i) == typeof(float))
            {
                row[dt.Columns[i].ColumnName] = reader.GetFloat(i);
            }
            else if (reader.GetFieldType(i) == typeof(Guid))
            {
                row[dt.Columns[i].ColumnName] = reader.GetGuid(i);
            }
            }

            dt.Rows.Add(row);
        }

        return dt;
    }

    // private static void FillDatatable (IDataReader reader, DataTable dt)  // ALTERNATIVE TO 'CREATEDATABASE'
    // {
    //     int len = reader.FieldCount;

    //     // Create the DataTable columns
    //     for (int i = 0; i < len; i++)
    //         dt.Columns.Add (reader.GetName (i), reader.GetFieldType (i));

    //     dt.BeginLoadData ();

    //     object[] values = new object[len];

    //     // Add data rows
    //     while (reader.Read ()) {
    //         for (int i = 0; i < len; i++)
    //             values[i] = reader[i];

    //         dt.Rows.Add(values);
    //     }

    //     dt.EndLoadData ();

    //     reader.Close ();
    //     reader.Dispose ();
    // }

    // public void Dispose() {
    //     dbcon.Close();
    //     Debug.Log("CLOSE_CONNECTION");
    // }
}


// class RunSQLCommand : IDisposable
// {
//     bool disposed;

//     public IDbConnection dbcon;
//     public IDbCommand dbcmd;

//     private IDataReader reader;

//     public RunSQLCommand(IDbConnection dbcon2, IDbCommand dbcmd2) {
//         dbcon = dbcon2;
//         dbcmd = dbcmd2;
//     }

//     public IDataReader RunCommand(string cmd) {
//         dbcon.Open();
//         dbcmd = dbcon.CreateCommand();
//         dbcmd.CommandText = cmd;
//         reader = dbcmd.ExecuteReader();
//         // dbcon.Close();
//         return reader;
//     }

//     protected virtual void Dispose(bool disposing) {
//         if (!disposed) {
//             if (disposing) {
//                 Debug.Log("CLOSE DATABSE");
//                 dbcon.Close();
//             }
//         }
//         disposed = true;
//     }

//     public void Dispose() {
//         Dispose(true);
//         GC.SuppressFinalize(this);
//     }

//     // ~RunCommand() {
//     //     Dispose();
//     // }
// }
