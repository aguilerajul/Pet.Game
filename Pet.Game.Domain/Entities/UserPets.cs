namespace Pet.Game.Domain.Entities
{
    public class UserPets
    {
        public virtual User User { get; private set; }
        public virtual Pet Pet { get; private set; }

        protected UserPets()
        {

        }

        public UserPets(User user, Pet pet)
        {
            this.User = user;
            this.Pet = pet;
        }
    }
}
