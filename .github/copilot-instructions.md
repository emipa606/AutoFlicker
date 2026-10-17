# Copilot Instructions for Auto Flicker Mod Development

## Mod Overview and Purpose

**Mod Name:** Auto Flicker  
**Author:** Taranchuk  
**Commissioned by:** Lucky  
**PackageId:** Mlie.AutoFlicker  

Auto Flicker is a mod designed to enhance and automate the process of toggling or "flicking" buildings in RimWorld that possess the flickable tag. It aims to provide a more seamless and automated experience in managing power and other toggle-dependent functions within the game. By supporting all buildings with the flickable tag, it ensures compatibility with both vanilla and modded buildings, thereby expanding its utility across various mod setups.

## Key Features and Systems

- **Automated Building Toggle:** Automatically toggles buildings that have the flickable tag based on game conditions.
- **Compatibility:** Works with both the base game and other mods, ensuring a broad range of supported structures.
- **User Interface:** Provides an ITab (Interface Tab) for managing flickable buildings directly from the UI.
- **Flexibility:** Supports a range of buildings, extending functionality to all flickable components in the game world.

## Coding Patterns and Conventions

### C# Code Structure

- **Types and Members:**
  - `Building_AutoFlicker.cs`: Main logic for handling flickable buildings.
    - Members include methods like `disableFlickablesAround`, `enableFlickablesAround`, and lifecycle methods such as `SpawnSetup`, `Tick`, and `ExposeData`.
  - `ITab_Flickables.cs`: Manages the user interface for flickable buildings.
    - Includes methods like `FillTab` and `IsVisible`.

- **Conventions:**
  - Consistency with naming and structure, following C# conventions for readability and maintainability.
  - Method names are verbose and descriptive of their function (`enableFlickablesAround`, `disableFlickablesAround`).
  - Lifecycle methods (`SpawnSetup`, `Tick`, etc.) are used to integrate with RimWorld's game loop.

## XML Integration

- **XML File Structure:**
  - `Buildings_Misc.xml` in `ThingDefs_Buildings`: Defines properties for flickable buildings.
    - These XML files are essential for registering new building types and their behaviors.
  - Translational templates for additional language support and localization efforts.

- **XML Conventions:**
  - Use of `ThingDef` to define new building behaviors and ensure compatibility.
  - Clear and structured XML that adheres to RimWorld's definition formats for ease of integration.

## Harmony Patching

- **Objective:** Use Harmony to patch game methods, ensuring that toggling behaviors are correctly injected into existing game logic.
- **Where Useful:** Primarily in extending or modifying existing game classes that interact with the flicking mechanism, ensuring the mod can automate or adjust how these systems behave without altering the core game files.
- **Considerations:** Maintain cross-compatibility with other mods by ensuring non-invasive patches and cleaning up any changes on mod disable.

## Suggestions for Copilot Usage

- **Code Completion:** Utilize Copilot for generating boilerplate code for common RimWorld modding tasks, such as defining defs or creating lifecycle methods.
- **Pattern Recognition:** Assist with harmonizing C# design patterns and XML structure across new additions to the mod.
- **Refactoring Suggestions:** Enhance readability and performance by suggesting improvements to existing code, particularly focusing on method extraction and single-responsibility principles.
- **Documentation:** Auto-generate comments and documentation within code files to maintain clarity, particularly around complex logic in `Building_AutoFlicker.cs`.

By adhering to these instructions, developers working on Auto Flicker can maintain a cohesive and functional mod while utilizing GitHub Copilot to streamline the development process.

## Project Solution Guidelines
- Relevant mod XML files are included as Solution Items under the solution folder named XML, these can be read and modified from within the solution.
- Use these in-solution XML files as the primary files for reference and modification.
- The `.github/copilot-instructions.md` file is included in the solution under the `.github` solution folder, so it should be read/modified from within the solution instead of using paths outside the solution. Update this file once only, as it and the parent-path solution reference point to the same file in this workspace.
- When making functional changes in this mod, ensure the documented features stay in sync with implementation; use the in-solution `.github` copy as the primary file.
- In the solution is also a project called Assembly-CSharp, containing a read-only version of the decompiled game source, for reference and debugging purposes.
- For any new documentation, update this copilot-instructions.md file rather than creating separate documentation files.


## Hard rules (must follow)
- Do NOT run commands that modify the repo (no git commit, git apply, dotnet format) unless explicitly asked.
- Prefer minimal reads: read only the smallest code region needed (around the suspicious lines).
- When mentioning SonarQube issues, automatically use the SonarQube MCP service to fetch and address issues instead of making inferred fixes without querying SonarQube first.
- When mentioning the rimworld log, automatically use the Rimworld MCP service to fetch the log.

