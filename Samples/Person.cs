using Data;

namespace DataAlchemist.Test
{
  [TableName("Owner", "Persons")]
  public class Person: BizObject 
  {
    private int64 _id; 
    private string _name;
    private int? age; //Define nullables for columns on nulls are allowed.
    
    [PrimaryKey]
    [Column("Id")]
    public int64? Id {
      get { return _id; }
      set { SetFieldValue("Id",value, ref _id); }
    }
    
    [Column("Name")]
    public string Name {
      get { return _name; }
      set { SetFieldValue("Name",value, ref _name); }
    }
    
    [Column("Age")]
    public string Age {
      get { return _age; }
      set { SetFieldValue("Age",value, ref _age); }
    }
  }
}