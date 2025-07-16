# Copilot Instructions for RimWorld Modding Project

## Mod Overview and Purpose

This RimWorld mod aims to enhance the player experience by automating certain tasks related to power management. Specifically, the mod introduces a system that automatically flicks power switches on or off based on certain conditions, thereby simplifying micromanagement for players. This mod is designed for use with the game version that supports .NET Framework 4.7.2 and above.

## Key Features and Systems

- **Automatic Power Flicking:** The mod introduces the `Building_AutoFlicker` class, which provides functionality to automatically enable or disable nearby flickable components based on game conditions.
- **Interface Integration:** Includes a custom ITab (`ITab_Flickables`) to allow players to manually interact with flickables and view the status of automated flicking.
- **XML Integration:** The mod seamlessly integrates with RimWorld's XML definition files for easy addition and modification of flickables and their properties.

## Coding Patterns and Conventions

- **File Naming:** Use PascalCase for class names and camelCase for method names, following C# conventions.
- **Class Design:** Each class is focused on a single responsibility — for example, `Building_AutoFlicker` manages the automation logic for flickable buildings.
- **Method Structure:** Keep methods concise and focused on one task. For example, `EnableFlickablesAround` and `DisableFlickablesAround` are self-explanatory and maintain single responsibility.

## XML Integration

- **Defining New Buildings:** XML files define the properties of new buildings that support the auto-flicker functionality. Ensure these XML files are properly referenced in the mod's `About` folder.
- **Patch Extensions:** Use XML extensions to patch existing game definitions where necessary, providing added layers of functionality or altering default behaviors.

## Harmony Patching

Harmony is used extensively to patch existing game functions that require modification for the mod’s features. Ensure patches are:

- **Non-Invasive:** Avoid altering existing game files; use Harmony’s patching capabilities to override or extend methods as necessary.
- **Well-Documented:** Each patch should have comments explaining the original method’s purpose and what changes the mod introduces through the patch.

## Suggestions for Copilot

- **Auto-Completion for Methods:** Use Copilot to suggest templates for new methods, especially those that deal with enabling/disabling components in the game.
- **XML Template Suggestions:** Make use of Copilot to draft property definitions in XML, allowing for quicker setup of new building definitions.
- **Patch Generation:** Utilize Copilot’s suggestions to create Harmony patches, ensuring that all necessary attributes and hooks are correctly set up.
- **Consistent Naming:** Leverage Copilot to maintain consistent naming conventions across the project, particularly when defining new classes and methods related to game logic.

By adhering to these guidelines, contributors can ensure that their additions to the mod are aligned with existing structures and conventions, promoting maintainability and coherence across the project.
