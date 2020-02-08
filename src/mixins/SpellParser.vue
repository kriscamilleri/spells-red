<script>
export default {
  name: "SpellParser",
  props: {
    spell: {
      id: Number,
      name: String,
      desc: Array,
      higher: String,
      range: String,
      ritual: Boolean,
      duration: String,
      conc: Boolean,
      casting: String,
      level: String,
      school: String,
      class: Array,
      source: String,
      page: String
    }
  },
  methods: {
    convertJsonArrayToHtml(jsonArr) {
      let text = this.splitMulti(jsonArr, ["'],['", "'], ['"]);
      text[0] = text[0].replace("['", "");
      text[text.length - 1] = text[text.length - 1].replace(/']/g, "");
      let html = `<p>${text.join("</p><p>")}</p>`;
      return html;
    },
    splitMulti(str, tokens) {
      var tempChar = tokens[0]; // We can use the first token as a temporary join character
      for (var i = 1; i < tokens.length; i++) {
        str = str.split(tokens[i]).join(tempChar);
      }
      str = str.split(tempChar);
      return str;
    },
    formattedSubtitle: function(spell) {
      return (
        this.formattedLevel(spell) +
        ", " +
        this.formattedRange(spell) +
        ",<br> " +
        this.formattedDuration(spell) +
        ", " +
        this.formattedcasting(spell) +
        "."
      );
    },
    formattedLevel: function(spell) {
      return "<strong class='text-danger'>" + spell.level + "</strong>";
    },
    formattedRange: function(spell) {
      return (
        "<strong class='text-info'>Range&nbsp;</strong>" +
        spell.range.replace("feet", "Feet")
      );
    },
    formattedDetails: function(spell) {
      let description = this.formattedDescription(spell);
      let classes = this.formattedClasses(spell);
      let components = this.formattedComponents(spell);
      return description + classes + components;
    },
    formattedClasses: function(spell) {
      // let classArr = spell.class;
      let classArray = spell.class.split(",");

      let classes =
        "<strong>Classes</strong><p>" + classArray.join(", ") + ".</p>";

      return classes;
    },
    formattedDescription: function(spell) {
      let description = this.convertJsonArrayToHtml(spell.desc);

      if (spell.higher) {
        let higher =
          "<span class='text-muted'>At Higher Levels </span>" +
          this.convertJsonArrayToHtml(spell.higher);
        description += higher;
      }
      return description;
    },
    formattedDuration: function(spell) {
      let result = "<strong class='text-warning'>Duration&nbsp;</strong>";
      if (spell.conc && spell.duration.length > 0) {
        let cleanedDuration = spell.duration;
        cleanedDuration = cleanedDuration.replace("conc, ", "");
        result += cleanedDuration[0].toUpperCase() + cleanedDuration.slice(1);
      } else {
        result += spell.duration;
      }
      return result; // capitalize first letter
    },
    formattedcasting: function(spell) {
      return (
        "<strong class='text-success'>Casting&nbsp;</strong>" + spell.casting
      );
    },
    formattedComponents: function(spell) {
      let array = [];
      if (spell.components.includes("V")) {
        array.push("Verbal");
      }
      if (spell.components.includes("S")) {
        array.push("Somatic");
      }
      if (spell.components.includes("M")) {
        array.push(`Material (${spell.material})`);
      }
      if (array.length > 0) {
        return "<p>" + array.join(", ") + "</p>";
      }
      return "";
    }
  }
};
</script>