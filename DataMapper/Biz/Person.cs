using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataMapper.Biz
{
    [TableName("","PERSON")]
    public class Person:Data.BizObject
    {
        string _name;
        string _surname;
        int? _age;
        int? _id;

        [Column("ID")]
        [PrimaryKey]
        public int? Id
        {
            get { return _id; }
            set { SetPropertyValue("Id", ref _id, value); }
        }
        [Column("NAME")]
        public string Name
        {
            get { return _name; }
            set { SetPropertyValue("Name", ref _name, value); }
        }
        [Column("SURNAME")]
        public string Surname
        {
            get { return _surname; }
            set { SetPropertyValue("Surname", ref _surname, value); }
        }
        [Column("AGE")]
        public int? Age
        {
            get { return _age; }
            set { SetPropertyValue("Age", ref _age, value); }
        }
    }
}
