using IoC.Attributes;

namespace CustomerLibrary
{
    [Export(typeof(ICustomerDAL))]
    public class CustomerDAL : ICustomerDAL
    {}

    public interface ICustomerDAL
    {}
}
