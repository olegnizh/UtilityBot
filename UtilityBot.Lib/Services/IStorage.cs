using UtilityBot.Lib.Models;

namespace UtilityBot.Lib.Services
{
    public interface IStorage
    {

        Session GetSession(long chatId);
		
    }
	
}