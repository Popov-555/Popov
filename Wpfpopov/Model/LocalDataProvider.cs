using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Wpfpopov.Model
{
    public class LocalDataProvider : IDataProvider
    {
        public IEnumerable<Students> GetStudents()
        {


            return new Students[]{
                new Students{Name="Popov Ilya Alecsandrovich",Date="21.11.2003",Telephone="89969588724", Course="2",Group="И-21"},
                new Students{Name="Petrov Ivan Victorovich",Date="11.11.2003",Telephone="89969987842", Course="3",Group="И-31"},
                new Students{Name="Fodorov Daniil Rovenkovich",Date="05.01.2004",Telephone="88752211221", Course="1",Group="И-11"}
            };
        }

        public IEnumerable<StudentsGroup> GetsStudentGroups()
        {

            return new StudentsGroup[] {
                new StudentsGroup { Title = "И-11" },
                new StudentsGroup { Title = "И-21" },
                new StudentsGroup { Title = "И-31" }
            };
        }
    }
}

                
        




    
    

        
    



