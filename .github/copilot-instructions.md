# Copilot Instructions

## Project Guidelines
- When scripting edits to .resx files (or other UTF-8 text files) via PowerShell, avoid Get-Content/[xml] cmdlets since they use the system codepage by default and corrupt non-ASCII characters (e.g. Arabic, Cyrillic, accented Latin). Use [System.IO.File]::ReadAllText/WriteAllText with explicit UTF8Encoding instead.