
using System;
using System.Collections.Generic;
using Mono.Data.Sqlite;
using UnityEngine;

namespace SQLite
{
    public class Testdb:MonoBehaviour
    {
        
        SqLiteHelper _testSqLiteHelper;
        
        //test-table name
        const string _TABLE_NAME = "test_t";
        
        void Start()
        {
            //测试select语句
            using (var test = new TestDatabase(_TABLE_NAME))
            {
                //SqlField selectFieldId=new SqlField("_id").SetType(SqlFieldType.Int).SetPrimaryKey(true);
                SqlField selectFieldName=new SqlField("_name").SetValue("updateN");
                SqlField selectFieldData=new SqlField("_data").SetValue("d4");
                SqlField selectFieldFloat=new SqlField("_floatField").SetType(SqlFieldType.Float).SetValue("3.5");
                SqlField selectFieldBlob=new SqlField("_blobField").SetType(SqlFieldType.Binary);
                
                //SqlField selectFieldCondition0=new SqlField("_name").SetValue("EnglishTest");
                
                //测试 bool select(SqlField x)
                //Debug.Log(test.Select(selectFieldCondition0) ? "Find" : "NotFind");
                
                string[] resultList = {"_id","_name","_data","_floatField","_blobField"};
                
                //单Field 单condition
                PrintData(test.Select("_data",selectFieldName));
                
                //多Field 单condition
                //PrintData(test.Select(resultList,selectFieldCondition0));
                
                //单Field 多condition
                //PrintData(test.Select("_blobField",SqlUnion.And,selectFieldFloat,selectFieldName));
                
                //多Field 多condition
                //PrintData(test.Select(resultList,SqlUnion.Or,selectFieldFloat,selectFieldName,selectFieldData));
                
            }

            //测试Insert
            /*using (var test = new TestDatabase(_TABLE_NAME))
            {
                SqlField addFieldName=new SqlField("_name").SetValue("addValue");
                SqlField addFieldData=new SqlField("_data").SetValue("addData");
                SqlField addFieldFloat=new SqlField("_floatField").SetValue("3.5").SetType(SqlFieldType.Float);
                SqlField addFieldBlob=new SqlField("_blobField").SetValue("3232test").SetType(SqlFieldType.Binary);
                test.Insert(addFieldName,addFieldData,addFieldFloat,addFieldBlob);
            }*/
            
            //测试delete
            /*using (var test = new TestDatabase(_TABLE_NAME))
            {
                //单个Field测试成功
                //SqlField deleteFieldFloat=new SqlField("_floatField").SetValue("2.8").SetType(SqlFieldType.Float);
                
                //多个Field
                SqlField deleteFieldFloat=new SqlField("_floatField").SetValue("3.1").SetType(SqlFieldType.Float);
                SqlField deleteFieldData=new SqlField("_data").SetValue("d1");

                SqlField[] deleteTest = {deleteFieldData, deleteFieldFloat};
                //test.Delete(deleteTest,SqlUnion.And);
                test.Delete(deleteTest,SqlUnion.Or);
            }*/

            /*using (var test = new TestDatabase(_TABLE_NAME))
            {
                SqlField updateFieldName=new SqlField("_name").SetValue("uMu");
                SqlField updateFieldData=new SqlField("_data").SetValue("dataMu");
                SqlField updateFieldCondition0=new SqlField("_blobField").SetValue("12").SetType(SqlFieldType.Binary);
                SqlField updateFieldCondition1=new SqlField("_floatField").SetValue("2.7").SetType(SqlFieldType.Float);

                SqlField[] updateFields = { updateFieldName,updateFieldData};
                //单Field 单condition
                //test.Update(updateFieldName,updateFieldCondition0);
                
                //单Field 多condition
                //test.Update(updateFieldName,SqlUnion.And,SqlOperation.Equal,updateFieldCondition0,updateFieldCondition1);
                
                //多Field 单condition
                //test.Update(updateFields,updateFieldCondition0);
                
                //多Field 多condition
                //test.Update(updateFields,SqlUnion.And,SqlOperation.Equal,updateFieldCondition0,updateFieldCondition1);
            }*/
            
        }


        static void PrintData(List<SqlField[]> result)
        {
            foreach (var tSqlFieldList in result)
            {
                foreach (var x in tSqlFieldList)
                {
                    Debug.Log(x.Name);
                    Debug.Log(x.Value);
                }
            }
        }

    }
}