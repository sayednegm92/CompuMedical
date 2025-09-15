using CompuMedical.Core.Helper;

namespace CompuMedical.Infrastructure.Helper;

public static class CommonExtenion
{
    public static List<DropDownList> GetEnumList<TEnum>() where TEnum : Enum
    {
        var dropdownListItems = Enum.GetValues(typeof(TEnum))
                                .Cast<TEnum>()
                                .Select(x => new DropDownList()
                                {
                                    Id = Convert.ToInt32(x),
                                    Name = x.ToString()
                                }).ToList();

        return dropdownListItems;
    }
}