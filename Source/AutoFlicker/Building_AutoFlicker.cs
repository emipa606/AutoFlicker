using System.Collections.Generic;
using System.Linq;
using RimWorld;
using Verse;

namespace AutoFlicker;

public class Building_AutoFlicker : Building
{
    public List<IntVec3> cellsToAffect;
    private CompPowerTrader powerComp;
    public List<Thing> thingsToIgnore = [];

    public override void SpawnSetup(Map map, bool respawningAfterLoad)
    {
        base.SpawnSetup(map, respawningAfterLoad);
        powerComp = GetComp<CompPowerTrader>();
        cellsToAffect = this.OccupiedRect().ExpandedBy(1).Cells.ToList();
        thingsToIgnore ??= [];
    }

    protected override void Tick()
    {
        base.Tick();
        switch (powerComp.PowerOn)
        {
            case true:
                enableFlickablesAround();
                break;
            case false:
                disableFlickablesAround();
                break;
        }
    }

    public override void DrawExtraSelectionOverlays()
    {
        base.DrawExtraSelectionOverlays();
        GenDraw.DrawFieldEdges(cellsToAffect);
    }

    private void enableFlickablesAround()
    {
        var things = new HashSet<CompFlickable>();
        foreach (var cell in cellsToAffect)
        {
            foreach (var thing in cell.GetThingList(Map))
            {
                if (thing == this || thingsToIgnore.Contains(thing) || thing is not ThingWithComps thingWithComps)
                {
                    continue;
                }

                var flickableComp = thingWithComps.GetComp<CompFlickable>();
                if (flickableComp is { SwitchIsOn: false })
                {
                    things.Add(flickableComp);
                }
            }
        }

        foreach (var thing in things)
        {
            thing.SwitchIsOn = true;
        }
    }

    private void disableFlickablesAround()
    {
        var things = new HashSet<CompFlickable>();
        foreach (var cell in cellsToAffect)
        {
            foreach (var thing in cell.GetThingList(Map))
            {
                if (thing == this || thingsToIgnore.Contains(thing) || thing is not ThingWithComps thingWithComps)
                {
                    continue;
                }

                var flickableComp = thingWithComps.GetComp<CompFlickable>();
                if (flickableComp is { SwitchIsOn: true })
                {
                    things.Add(flickableComp);
                }
            }
        }

        foreach (var thing in things)
        {
            thing.SwitchIsOn = false;
        }
    }

    protected override void ReceiveCompSignal(string signal)
    {
        base.ReceiveCompSignal(signal);
        switch (signal)
        {
            case "FlickedOff":
                disableFlickablesAround();
                break;
            case "FlickedOn":
                enableFlickablesAround();
                break;
        }
    }

    public override void ExposeData()
    {
        base.ExposeData();
        Scribe_Collections.Look(ref thingsToIgnore, "thingsToIgnore", LookMode.Reference);
    }
}