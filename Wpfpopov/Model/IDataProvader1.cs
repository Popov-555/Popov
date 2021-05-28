using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Wpfpopov.Model
{
    interface IDataProvider
    {
        IEnumerable<Students> GetStudents();
        IEnumerable<StudentsGroup> GetsStudentGroups();

    }
}
