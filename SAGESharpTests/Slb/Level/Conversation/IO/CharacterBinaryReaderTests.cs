﻿using Moq;
using NUnit.Framework;
using SAGESharp.Slb;
using SAGESharp.Slb.IO;
using SAGESharp.Slb.Level.Conversation;
using SAGESharp.Slb.Level.Conversation.IO;
using System;
using System.IO;

namespace SAGESharpTests.Slb.Level.Conversation.IO
{
    [TestFixture]
    public static class CharacterBinaryReaderTests
    {
        [Test]
        public static void TestCharacterBinaryReaderConstructor()
        {
            var stream = new Mock<Stream>().Object;
            var identifierReader = new Mock<ISlbReader<Identifier>>().Object;
            var infoReader = new Mock<ISlbReader<Info>>().Object;

            Assert.That(() => new CharacterBinaryReader(null, identifierReader, infoReader), Throws.InstanceOf<ArgumentNullException>());
            Assert.That(() => new CharacterBinaryReader(stream, null, infoReader), Throws.InstanceOf<ArgumentNullException>());
            Assert.That(() => new CharacterBinaryReader(stream, identifierReader, null), Throws.InstanceOf<ArgumentNullException>());
        }

        [Test]
        public static void TestReadCharacterSlb()
        {
            var streamMock = new Mock<Stream>();
            var identifierReaderMock = new Mock<ISlbReader<Identifier>>();
            var infoReaderMock = new Mock<ISlbReader<Info>>();

            var reader = new CharacterBinaryReader(streamMock.Object, identifierReaderMock.Object, infoReaderMock.Object);

            var toaName = new Identifier(0x11223344);
            var charName = new Identifier(0x11223345);
            var charCont = new Identifier(0x11223346);

            identifierReaderMock
                .SetupSequence(identifierReader => identifierReader.ReadSlbObject())
                .Returns(toaName)
                .Returns(charName)
                .Returns(charCont);

            // Info entry count
            streamMock
                .SetupSequence(stream => stream.ReadByte())
                .Returns(0x02)
                .Returns(0x00)
                .Returns(0x00)
                .Returns(0x00);

            var info1 = new Info();
            var info2 = new Info();
            infoReaderMock
                .SetupSequence(infoReader => infoReader.ReadSlbObject())
                .Returns(info1)
                .Returns(info2);

            var character = reader.ReadSlbObject();

            Assert.AreEqual(character.ToaName, toaName);
            Assert.AreEqual(character.CharName, charName);
            Assert.AreEqual(character.CharCont, charCont);
            Assert.AreEqual(character.Entries.Count, 2);
            Assert.IsTrue(character.Entries.Contains(info1));
            Assert.IsTrue(character.Entries.Contains(info2));

            streamMock.Verify(stream => stream.ReadByte(), Times.Exactly(4));
            streamMock.VerifyNoOtherCalls();

            identifierReaderMock.Verify(identifierReader => identifierReader.ReadSlbObject(), Times.Exactly(3));
            identifierReaderMock.VerifyNoOtherCalls();

            infoReaderMock.Verify(infoReader => infoReader.ReadSlbObject(), Times.Exactly(2));
            infoReaderMock.VerifyNoOtherCalls();
        }
    }
}
