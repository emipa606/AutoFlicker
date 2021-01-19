using RimWorld;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using Verse;

namespace AutoFlicker
{
	public class ITab_Flickables : ITab
	{
		private Vector2 scrollPosition;

		private static readonly Vector2 WinSize = new Vector2(300f, 480f);
		public override bool IsVisible
		{
			get
			{
				Building_AutoFlicker thing = base.SelObject as Building_AutoFlicker;
				if (thing != null && thing.Faction == Faction.OfPlayer)
				{
					return true;
				}
				return false;
			}
		}

		public ITab_Flickables()
		{
			size = WinSize;
			labelKey = "AF.Flickables";
		}

		protected override void FillTab()
		{
			Rect position = new Rect(0f, 0f, WinSize.x, WinSize.y).ContractedBy(10f);
			var autoFlicker = base.SelObject as Building_AutoFlicker;
			var yPos = 15;
			var things = new HashSet<Thing>();

			foreach (var cell in autoFlicker.cellsToAffect)
			{
				foreach (var thing in cell.GetThingList(autoFlicker.Map))
				{
					if (thing != autoFlicker && thing is ThingWithComps thingWithComps && thingWithComps.GetComp<CompFlickable>() != null)
					{
						things.Add(thing);
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
				Rect iconRect = new Rect(rect.x, rect.y, 27, 27);
				Widgets.ThingIcon(iconRect, thing, 1f);
				Rect labelRect = new Rect(iconRect.xMax + 10, rect.y, rect.width - (iconRect.xMax + 10), 27);
				Widgets.Label(labelRect, thing.LabelCap);
				bool keepFlickering = !autoFlicker.thingsToIgnore.Contains(thing);
				Widgets.Checkbox(labelRect.xMax + 15, rect.y, ref keepFlickering);
				if (!keepFlickering && !autoFlicker.thingsToIgnore.Contains(thing))
				{
					autoFlicker.thingsToIgnore.Add(thing);
				}
				else if (keepFlickering && autoFlicker.thingsToIgnore.Contains(thing))
				{
					autoFlicker.thingsToIgnore.Remove(thing);
				}
				yPos += 30;
			}
		}
	}
}
