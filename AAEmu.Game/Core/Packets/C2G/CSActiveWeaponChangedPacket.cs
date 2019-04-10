using AAEmu.Commons.Network;
using AAEmu.Game.Core.Network.Game;
using AAEmu.Game.Core.Packets.G2C;

namespace AAEmu.Game.Core.Packets.C2G
{
    public class CSActiveWeaponChangedPacket : GamePacket
    {
        public CSActiveWeaponChangedPacket() : base(0x08c, 1) //TODO 1.0 opcode: 0x08b
        {
        }

        public override void Read(PacketStream stream)
        {
            var activeWeapon = stream.ReadByte();
            Connection.ActiveChar.ActiveWeapon = activeWeapon;
            Connection.ActiveChar.BroadcastPacket(new SCActiveWeaponChangedPacket(Connection.ActiveChar.ObjId, activeWeapon), true);
        }
    }
}
