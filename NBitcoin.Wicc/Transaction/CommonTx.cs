﻿using System;
using NBitcoin.Protocol;
using NBitcoin.Wicc.Core;

namespace NBitcoin.Wicc.Transaction
{
    public class CommonTx : BaseTransaction
    {
        public CommonTx()
        {
            TxType = (ulong)TxTypeConst.COMMON_TX;
        }

        public UserId SrcId;
        public UserId DesId;
        public UInt64 Fees;
        public UInt64 Values;
        public VarString Contract;

        public override void ReadWrite(BitcoinStream stream)
        {
            stream.ReadWriteAsVarInt(ref TxType);
            stream.ReadWriteAsVarInt(ref Version);
            stream.ReadWriteAsCompactVarInt(ref ValidHeight);
            stream.ReadWrite(ref SrcId);
            stream.ReadWrite(ref DesId);
            stream.ReadWriteAsCompactVarInt(ref Fees);
            stream.ReadWriteAsCompactVarInt(ref Values);
            stream.ReadWrite(ref Contract);
            stream.ReadWrite(ref Signature);
        }

        public override uint256 GetSignatureHash()
        {
            BitcoinStream stream = CreateHashWriter(HashVersion.Original);

            stream.ReadWriteAsVarInt(ref Version);
            stream.ReadWriteAsVarInt(ref TxType);
            stream.ReadWriteAsCompactVarInt(ref ValidHeight);
            stream.ReadWrite(ref SrcId);
            stream.ReadWrite(ref DesId);
            stream.ReadWriteAsCompactVarInt(ref Fees);
            stream.ReadWriteAsCompactVarInt(ref Values);
            stream.ReadWrite(ref Contract);

            return GetHash(stream);
        }
    }
}
