using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlarmMonitorSystem.Util
{
    public class JsonUtility
    {
        public static string? DataTableToJsonString(DataTable dt, int offset = 0, int limit = 100000)
        {
            DataSet ds = new DataSet();
            ds.Merge(dt);
            StringBuilder JsonString = new StringBuilder(1024 * 1024 * 10);
            if (ds != null && ds.Tables[0].Rows.Count > 0)
            {
                JsonString.Append("[");
                for (int i = offset; i < ds.Tables[0].Rows.Count && i < (offset + limit); i++)
                {
                    JsonString.Append("{");
                    for (int j = 0; j < ds.Tables[0].Columns.Count; j++)
                    {
                        string tag = ds.Tables[0].Columns[j].ColumnName.ToString();
                        string? value = null;
                        if (ds.Tables[0].Rows[i][j] != DBNull.Value)
                            value = ds.Tables[0].Rows[i][j].ToString();

                        if (value != null)
                        {
                            string quotestr = "";
                            if (ds.Tables[0].Columns[j].DataType.Equals(typeof(DateTime)) || ds.Tables[0].Columns[j].DataType.Equals(typeof(String)))
                                quotestr = "\"";
                            if (j < ds.Tables[0].Columns.Count - 1)
                            {
                                JsonString.Append("\"" + tag + "\":" + quotestr + value + quotestr + ", ");
                            }
                            else if (j == ds.Tables[0].Columns.Count - 1)
                            {
                                JsonString.Append("\"" + tag + "\":" + quotestr + value + quotestr);
                            }
                        }
                    }
                    if (i == ds.Tables[0].Rows.Count - 1)
                    {
                        JsonString.Append("}");
                    }
                    else
                    {
                        JsonString.Append("},");
                    }
                }
                JsonString.Append("]");
                return JsonString.ToString();
            }
            else
            {
                return null;
            }
        }
    }
}