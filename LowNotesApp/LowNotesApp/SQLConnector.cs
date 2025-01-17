﻿using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Linq;
using Dapper;
using System.Data;

namespace LowNotesApp
{
    public class SQLConnector
    {
        public string GetNotes(string NoteName)
        {
            using (IDbConnection connection = new SqlConnection(@"Server=DESKTOP-QPBOC36\SQLEXPRESS; Database=SynthNotes; Trusted_Connection=True;"))
            //new SqlConnection(Helper.CnnVal("SynthNotes")))
            {
                var notesList= connection.Query<Note>($"SELECT [NoteFrequency] FROM [SynthNotes].[dbo].[LowNotes] WHERE NoteName = '{ NoteName }'").ToList();
                return notesList[0].NoteFrequency;
            }
        }
    }
}
