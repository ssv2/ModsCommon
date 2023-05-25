﻿using ColossalFramework;
using ColossalFramework.UI;
using HarmonyLib;
using ModsCommon.UI;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;

namespace ModsCommon
{
    public static partial class Patcher
    {
        public static IEnumerable<CodeInstruction> ToolControllerAwakeTranspiler<TypeMod, TypeTool>(ILGenerator generator, IEnumerable<CodeInstruction> instructions)
            where TypeMod : ICustomMod
            where TypeTool : BaseTool<TypeMod, TypeTool>
        {
            var createMethod = AccessTools.Method(typeof(TypeTool), nameof(BaseTool<TypeMod, TypeTool>.Create));
            yield return new CodeInstruction(OpCodes.Call, createMethod);

            foreach (var instruction in instructions)
                yield return instruction;
        }

        public static IEnumerable<CodeInstruction> GameKeyShortcutsEscapeTranspiler<TypeMod, TypeTool>(ILGenerator generator, IEnumerable<CodeInstruction> instructions)
            where TypeMod : ICustomMod
            where TypeTool : BaseTool<TypeMod, TypeTool>
        {
            var returnLabel = generator.DefineLabel();
            var elseLabel = generator.DefineLabel();
            var getExist = AccessTools.PropertyGetter(typeof(Singleton<InfoManager>), nameof(Singleton<InfoManager>.exists));

            foreach (var instruction in instructions)
            {
                if (instruction.opcode == OpCodes.Call && instruction.operand == getExist)
                {
                    yield return new CodeInstruction(OpCodes.Call, AccessTools.PropertyGetter(typeof(SingletonTool<TypeTool>), nameof(SingletonTool<TypeTool>.Instance))) { labels = instruction.labels };
                    yield return new CodeInstruction(OpCodes.Call, AccessTools.PropertyGetter(typeof(TypeTool), nameof(BaseTool<TypeMod, TypeTool>.enabled)));
                    yield return new CodeInstruction(OpCodes.Brfalse, elseLabel);

                    yield return new CodeInstruction(OpCodes.Call, AccessTools.PropertyGetter(typeof(SingletonTool<TypeTool>), nameof(SingletonTool<TypeTool>.Instance)));
                    yield return new CodeInstruction(OpCodes.Callvirt, AccessTools.Method(typeof(TypeTool), nameof(BaseTool<TypeMod, TypeTool>.Escape)));
                    yield return new CodeInstruction(OpCodes.Br, returnLabel);

                    instruction.labels = new List<Label>() { elseLabel };
                }
                else if (instruction.opcode == OpCodes.Ret)
                    instruction.labels.Add(returnLabel);

                yield return instruction;
            }
        }

        public static void GeneratedScrollPanelCreateOptionPanelPostfix<TypeMod, TypeButton>(string templateName, ref OptionPanelBase __result, params string[] allow)
            where TypeMod : ICustomMod
            where TypeButton : CustomUIButton
        {
            if (__result == null || !allow.Any(i => i == templateName))
                return;

            if (__result.component.parent.Find<TypeButton>(typeof(TypeButton).Name) is not TypeButton button)
            {
                button = __result.component.parent.AddUIComponent<TypeButton>();
                button.isVisible = false;
            }

            __result.component.eventVisibilityChanged += (_, visible) => button.isVisible = visible;
        }
        public static string RoadsOptionPanel => nameof(RoadsOptionPanel);
        public static string PathsOptionPanel => nameof(PathsOptionPanel);
        public static string TracksOptionPanel => nameof(TracksOptionPanel);
        public static string CanalsOptionPanel => nameof(CanalsOptionPanel);
        public static string QuaysOptionPanel => nameof(QuaysOptionPanel);
        public static string FloodWallsOptionPanel => nameof(FloodWallsOptionPanel);
    }
}
