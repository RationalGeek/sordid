using NUnit.Framework;
using Sordid.Core;
using Sordid.Core.Model;
using System.Linq;

namespace Sordid.Tests.Database
{
    [TestFixture]
    public class CharacterCrud
    {
        private SordidDbContext _ctx;
        private Character _character;

        [SetUp]
        public void SetUp()
        {
            _ctx = new SordidDbContext();
            var randomGoodUserId = _ctx.Users.First().Id;
            _character = new Character { Name = "Test Character", ApplicationUserId = randomGoodUserId };
        }

        [Test]
        public void CharacterCrud_Create()
        {
            _ctx.Characters.Add(_character);
            _ctx.SaveChanges();
            Assert.AreNotEqual(0, _character.Id);
        }

        [Test]
        public void CharacterCrud_Update()
        {
            _ctx.Characters.Add(_character);
            _ctx.SaveChanges();

            _ctx = new SordidDbContext();
            _character = _ctx.Characters.Where(c => c.Id == _character.Id).First();
            _character.Name = "Updated!";
            _ctx.SaveChanges();

            _ctx = new SordidDbContext();
            _character = _ctx.Characters.Where(c => c.Id == _character.Id).First();
            Assert.AreEqual("Updated!", _character.Name);
        }

        [Test]
        public void CharacterCrud_Delete()
        {
            _ctx.Characters.Add(_character);
            _ctx.SaveChanges();

            _ctx = new SordidDbContext();
            _character = _ctx.Characters.Where(c => c.Id == _character.Id).First();
            _ctx.Characters.Remove(_character);
            _ctx.SaveChanges();

            _ctx = new SordidDbContext();
            _character = _ctx.Characters.Where(c => c.Id == _character.Id).FirstOrDefault();
            Assert.IsNull(_character);
        }
    }
}
