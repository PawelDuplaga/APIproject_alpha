using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Breakfast.Conctracts.Breakfast
{
    public record BreakfastResponse(
      string Name,
      string Description,
      DateTime StartDateTime,
      DateTime EndDateTime,
      DateTime LastModifiedDateTime,
      List<string> Savory,
      List<string> Sweet
  );

}
