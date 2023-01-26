using System;
using System.Collections.Generic;
using System.Text;
using SQLite;

namespace Capremci
{
    public interface DataBase
    {

        SQLiteAsyncConnection GetConnection();
    }
}
