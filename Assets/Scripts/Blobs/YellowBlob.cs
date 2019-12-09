using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class YellowBlob : DumbBlob
{
    public override bool canCollect(int numBlobs)
    {
        return numBlobs > 0 && !_isCollected && isCollectable;
    }

    protected override void Think()
    {
        base.Think();
    }
}
