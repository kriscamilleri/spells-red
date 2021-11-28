namespace SpellsRedApi
{

    public partial class Repository
    {
        public string Name { get;set; }
        public string Path { get; set; }
        public string Source { get; set; }

    }

    public partial class Spells
    {
        public List<Spell> Spell { get; set; }
    }

    public partial class Spell
    {
        public string Name { get; set; }
        public string Source { get; set; }
        public long Page { get; set; }
        public Srd? Srd { get; set; }
        public long Level { get; set; }
        public string School { get; set; }
        public List<Time> Time { get; set; }
        public Range Range { get; set; }
        public Components Components { get; set; }
        public List<DurationElement> Duration { get; set; }
        public List<SpellEntry> Entries { get; set; }
        public ScalingLevelDice ScalingLevelDice { get; set; }
        public List<string> DamageInflict { get; set; }
        public List<string> SavingThrow { get; set; }
        public List<string> MiscTags { get; set; }
        public List<string> AreaTags { get; set; }
        public Classes Classes { get; set; }
        public List<Background> Backgrounds { get; set; }
        public List<EntriesHigherLevel> EntriesHigherLevel { get; set; }
        public List<Race> Races { get; set; }
        public Meta Meta { get; set; }
        public List<Background> EldritchInvocations { get; set; }
        public List<string> ConditionInflict { get; set; }
        public List<string> DamageResist { get; set; }
        public bool? HasFluffImages { get; set; }
        public List<string> SpellAttack { get; set; }
        public List<string> AbilityCheck { get; set; }
        public List<string> ConditionImmune { get; set; }
        public List<string> DamageVulnerable { get; set; }
        public List<string> DamageImmune { get; set; }
    }

    public partial class Background
    {
        public string Name { get; set; }
        public string Source { get; set; }
    }

    public partial class Classes
    {
        public List<Background> FromClassList { get; set; }
        public List<FromClassListVariant> FromClassListVariant { get; set; }
        public List<FromSubclass> FromSubclass { get; set; }
    }

    public partial class FromClassListVariant
    {
        public string Name { get; set; }
        public string Source { get; set; }
        public string DefinedInSource { get; set; }
    }

    public partial class FromSubclass
    {
        public Background Class { get; set; }
        public Subclass Subclass { get; set; }
    }

    public partial class Subclass
    {
        public string Name { get; set; }
        public string Source { get; set; }
        public string SubSubclass { get; set; }
    }

    public partial class Components
    {
        public bool? V { get; set; }
        public bool? S { get; set; }
        public MUnion? M { get; set; }
    }

    public partial class MClass
    {
        public string Text { get; set; }
        public long? Cost { get; set; }
        public bool? Consume { get; set; }
    }

    public partial class DurationElement
    {
        public string Type { get; set; }
        public DurationDuration Duration { get; set; }
        public bool? Concentration { get; set; }
        public List<string> Ends { get; set; }
    }

    public partial class DurationDuration
    {
        public string Type { get; set; }
        public long Amount { get; set; }
        public bool? UpTo { get; set; }
    }

    public partial class PurpleEntry
    {
        public string Type { get; set; }
        public string Name { get; set; }
        public List<EntryEntryUnion> Entries { get; set; }
        public string Caption { get; set; }
        public List<string> ColLabels { get; set; }
        public List<string> ColStyles { get; set; }
        public List<List<RowElement>> Rows { get; set; }
        public List<string> Items { get; set; }
    }

    public partial class FluffyEntry
    {
        public string Type { get; set; }
        public List<string> Items { get; set; }
    }

    public partial class RowClass
    {
        public string Type { get; set; }
        public Roll Roll { get; set; }
    }

    public partial class Roll
    {
        public long? Exact { get; set; }
        public long? Min { get; set; }
        public long? Max { get; set; }
        public bool? Pad { get; set; }
    }

    public partial class EntriesHigherLevel
    {
        public string Type { get; set; }
        public string Name { get; set; }
        public List<string> Entries { get; set; }
    }

    public partial class Meta
    {
        public bool Ritual { get; set; }
    }

    public partial class Race
    {
        public string Name { get; set; }
        public string Source { get; set; }
        public string BaseName { get; set; }
        public string BaseSource { get; set; }
    }

    public partial class Range
    {
        public string Type { get; set; }
        public Distance Distance { get; set; }
    }

    public partial class Distance
    {
        public string Type { get; set; }
        public long? Amount { get; set; }
    }

    public partial class ScalingLevelDice
    {
        public string Label { get; set; }
        public Scaling Scaling { get; set; }
    }

    public partial class Scaling
    {
        public string The1 { get; set; }
        public string The5 { get; set; }
        public string The11 { get; set; }
        public string The17 { get; set; }
    }

    public partial class Time
    {
        public long Number { get; set; }
        public string Unit { get; set; }
        public string Condition { get; set; }
    }

    public partial struct MUnion
    {
        public MClass MClass { get; set; }
        public string String { get; set; }

        public static implicit operator MUnion(MClass MClass) => new MUnion { MClass = MClass };
        public static implicit operator MUnion(string String) => new MUnion { String = String };
    }

    public partial struct EntryEntryUnion
    {
        public FluffyEntry FluffyEntry { get; set; }
        public string String { get; set; }

        public static implicit operator EntryEntryUnion(FluffyEntry FluffyEntry) => new EntryEntryUnion { FluffyEntry = FluffyEntry };
        public static implicit operator EntryEntryUnion(string String) => new EntryEntryUnion { String = String };
    }

    public partial struct RowElement
    {
        public RowClass RowClass { get; set; }
        public string String { get; set; }

        public static implicit operator RowElement(RowClass RowClass) => new RowElement { RowClass = RowClass };
        public static implicit operator RowElement(string String) => new RowElement { String = String };
    }

    public partial class SpellEntry
    {
        public PurpleEntry PurpleEntry { get; set; }
        public string String { get; set; }

        public static implicit operator SpellEntry(PurpleEntry PurpleEntry) => new SpellEntry { PurpleEntry = PurpleEntry };
        public static implicit operator SpellEntry(string String) => new SpellEntry { String = String };
    }

    public partial struct Srd
    {
        public bool? Bool { get; set; }
        public string String { get; set; }

        public static implicit operator Srd(bool Bool) => new Srd { Bool = Bool };
        public static implicit operator Srd(string String) => new Srd { String = String };
    }
}
