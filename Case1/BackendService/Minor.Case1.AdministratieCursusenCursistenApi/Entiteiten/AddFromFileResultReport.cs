using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Minor.Case1.AdministratieCursusenCursistenApi.Entiteiten
{
    public class AddFromFileResultReport
    {
        public string ErrorMessage { get; private set; }
        public int AantalAddedItems { get; private set; }
        public int AantalDuplicateItems { get; private set; }

        public AddFromFileResultReport(string message)
        {
            ErrorMessage = message;
        }

        public AddFromFileResultReport(int nAddedItems, int nDuplicateItems)
        {
            AantalAddedItems = nAddedItems;
            AantalDuplicateItems = nDuplicateItems;
        }
    }
}
