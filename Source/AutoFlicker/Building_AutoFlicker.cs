﻿using RimWorld;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Verse;

namespace AutoFlicker
{
    public class Building_AutoFlicker : Building
    {
        private CompPowerTrader powerComp;
        public List<IntVec3> cellsToAffect;
        public List<Thing> thingsToIgnore = new List<Thing>();
        public override void SpawnSetup(Map map, bool respawningAfterLoad)
        {
            base.SpawnSetup(map, respawningAfterLoad);
            powerComp = GetComp<CompPowerTrader>();
            cellsToAffect = this.OccupiedRect().ExpandedBy(1).Cells.ToList();
            if (thingsToIgnore is null)
            {
                thingsToIgnore = new List<Thing>();
            }
        }
        public override void Tick()
        {
            base.Tick();
            if (powerComp.PowerOn)
            {
                EnableFlickablesAround();
            }
            else if (!powerComp.PowerOn)
            {
                DisableFlickablesAround();
            }
        }

        public override void DrawExtraSelectionOverlays()
        {
            base.DrawExtraSelectionOverlays();
            GenDraw.DrawFieldEdges(cellsToAffect);
        }

        public void EnableFlickablesAround()
        {
            var things = new HashSet<CompFlickable>();
            foreach (var cell in cellsToAffect)
            {
                foreach (var thing in cell.GetThingList(Map))
                {
                    if (thing != this && !thingsToIgnore.Contains(thing) && thing is ThingWithComps thingWithComps)
                    {
                        var flickableComp = thingWithComps.GetComp<CompFlickable>();
                        if (flickableComp != null && !flickableComp.SwitchIsOn)
                        {
                            things.Add(flickableComp);
                        }
                    }
                }
            }

            foreach (var thing in things)
            {
                thing.SwitchIsOn = true;
            }
        }

        public void DisableFlickablesAround()
        {
            var things = new HashSet<CompFlickable>();
            foreach (var cell in cellsToAffect)
            {
                foreach (var thing in cell.GetThingList(Map))
                {
                    if (thing != this && !thingsToIgnore.Contains(thing) && thing is ThingWithComps thingWithComps)
                    {
                        var flickableComp = thingWithComps.GetComp<CompFlickable>();
                        if (flickableComp != null && flickableComp.SwitchIsOn)
                        {
                            things.Add(flickableComp);
                        }
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
            if ("FlickedOff" == signal)
            {
                DisableFlickablesAround();
            }
            else if (signal == "FlickedOn")
            {
                EnableFlickablesAround();
            }
        }

        public override void ExposeData()
        {
            base.ExposeData();
            Scribe_Collections.Look(ref thingsToIgnore, "thingsToIgnore", LookMode.Reference);
        }
    }
}