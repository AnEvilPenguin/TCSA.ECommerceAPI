namespace ECommerceAPI.Services.Files;

// An interesting problem here
// Could probably just have a single generic interface ISeeder or something,
// But how would we get the correct seeder in when required?
// We might have to have an abstraction layer based on concrete classes?
// For things like databases that should be easy enough, the whole system would use the same database
// Here I'm allowing for multiple different types...
// I like the detection of file types, but it does come at a cost here...
public interface ICsvSeeder : ISeeder
{
}