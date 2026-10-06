using System;

namespace SupportToolsServerCore.Domain.StoredFiles;

//ფაილის მეტამონაცემები შიგთავსის გარეშე: ფაილების სია ბაზიდან შიგთავსს (nvarchar(max)) არ კითხულობს
public sealed record StoredFileInfo(string Path, string Sha256, int Length, DateTime UpdatedAtUtc, int Version);
