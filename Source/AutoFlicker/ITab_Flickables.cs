using System.Collections.Generic;
using RimWorld;
using UnityEngine;
using Verse;

namespace AutoFlicker;

public class ITab_Flickables : ITab
{
    private static readonly Vector2 winSize = new(300f, 480f);

    public ITab_Flickables()
    {
        size = winSize;
        labelKey = "AF.Flickables";
    }

    public override bool IsVisible => SelObject is Building_AutoFlicker thing && thing.Faction == Faction.OfPlayer;

    protected override void FillTab()
    {
        var position = new Rect(0f, 0f, winSize.x, winSize.y).ContractedBy(10f);
        var autoFlicker = SelObject as Building_AutoFlicker;
        var yPos = 15;
        var things = new HashSet<Thing>();

        if (autoFlicker?.cellsToAffect != null)
        {
            foreach (var cell in autoFlicker.cellsToAffect)
            {
                foreach (var thing in cell.GetThingList(autoFlicker.Map))
                {
                    if (thing != autoFlicker && thing is ThingWithComps thingWithComps &&
                        thingWithComps.GetComp<CompFlickable>() != null)
                    {
                        things.Add(thing);
                    }
                }
            }
        }

        foreach (var thing in things)
        {
            var rect = new Rect(15, yPos, position.width - 45, 27);
            if (Mouse.IsOver(rect))
            {
                Widgets.DrawHighlight(rect);
            }

            var iconRect = new Rect(rect.x, rect.y, 27, 27);
            Widgets.ThingIcon(iconRect, thing);
            var labelRect = new Rect(iconRect.xMax + 10, rect.y, rect.width - (iconRect.xMax + 10), 27);
            Widgets.Label(labelRect, thing.LabelCap);
            var keepFlickering = autoFlicker != null && !autoFlicker.thingsToIgnore.Contains(thing);
            Widgets.Checkbox(labelRect.xMax + 15, rect.y, ref keepFlickering);
            if (autoFlicker != null && !keepFlickering && !autoFlicker.thingsToIgnore.Contains(thing))
            {
                autoFlicker.thingsToIgnore.Add(thing);
            }
            else if (autoFlicker != null && keepFlickering && autoFlicker.thingsToIgnore.Contains(thing))
            {
                autoFlicker.thingsToIgnore.Remove(thing);
            }

            yPos += 30;
        }
    }
}