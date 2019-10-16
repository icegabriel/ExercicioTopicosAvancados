using System;
using System.Collections.Generic;
using System.Text;

namespace DietaProjecao.Models
{
    public class Humidity
    {
        public string qty { get; set; }
        public string unit { get; set; }
    }

    public class Protein
    {
        public string qty { get; set; }
        public string unit { get; set; }
    }

    public class Lipid
    {
        public string qty { get; set; }
        public string unit { get; set; }
    }

    public class Cholesterol
    {
        public string qty { get; set; }
        public string unit { get; set; }
    }

    public class Carbohydrate
    {
        public string qty { get; set; }
        public string unit { get; set; }
    }

    public class Fiber
    {
        public string qty { get; set; }
        public string unit { get; set; }
    }

    public class Ashes
    {
        public string qty { get; set; }
        public string unit { get; set; }
    }

    public class Calcium
    {
        public string qty { get; set; }
        public string unit { get; set; }
    }

    public class Magnesium
    {
        public string qty { get; set; }
        public string unit { get; set; }
    }

    public class Phosphorus
    {
        public string qty { get; set; }
        public string unit { get; set; }
    }

    public class Iron
    {
        public string qty { get; set; }
        public string unit { get; set; }
    }

    public class Sodium
    {
        public string qty { get; set; }
        public string unit { get; set; }
    }

    public class Potassium
    {
        public string qty { get; set; }
        public string unit { get; set; }
    }

    public class Copper
    {
        public string qty { get; set; }
        public string unit { get; set; }
    }

    public class Zinc
    {
        public string qty { get; set; }
        public string unit { get; set; }
    }

    public class Retinol
    {
        public string qty { get; set; }
        public string unit { get; set; }
    }

    public class Thiamine
    {
        public string qty { get; set; }
        public string unit { get; set; }
    }

    public class Riboflavin
    {
        public string qty { get; set; }
        public string unit { get; set; }
    }

    public class Pyridoxine
    {
        public string qty { get; set; }
        public string unit { get; set; }
    }

    public class Niacin
    {
        public string qty { get; set; }
        public string unit { get; set; }
    }

    public class Energy
    {
        public string kcal { get; set; }
        public string kj { get; set; }
    }

    public class Saturated
    {
        public string qty { get; set; }
        public string unit { get; set; }
    }

    public class Monounsaturated
    {
        public string qty { get; set; }
        public string unit { get; set; }
    }

    public class Polyunsaturated
    {
        public string qty { get; set; }
        public string unit { get; set; }
    }

    public class FattyAcids
    {
        public Saturated saturated { get; set; }
        public Monounsaturated monounsaturated { get; set; }
        public Polyunsaturated polyunsaturated { get; set; }
    }

    public class Manganese
    {
        public string qty { get; set; }
        public string unit { get; set; }
    }

    public class Attributes
    {
        public Humidity humidity { get; set; }
        public Protein protein { get; set; }
        public Lipid lipid { get; set; }
        public Cholesterol cholesterol { get; set; }
        public Carbohydrate carbohydrate { get; set; }
        public Fiber fiber { get; set; }
        public Ashes ashes { get; set; }
        public Calcium calcium { get; set; }
        public Magnesium magnesium { get; set; }
        public Phosphorus phosphorus { get; set; }
        public Iron iron { get; set; }
        public Sodium sodium { get; set; }
        public Potassium potassium { get; set; }
        public Copper copper { get; set; }
        public Zinc zinc { get; set; }
        public Retinol retinol { get; set; }
        public Thiamine thiamine { get; set; }
        public Riboflavin riboflavin { get; set; }
        public Pyridoxine pyridoxine { get; set; }
        public Niacin niacin { get; set; }
        public Energy energy { get; set; }
        public FattyAcids fatty_acids { get; set; }
        public Manganese manganese { get; set; }
    }

    public class Alimento
    {
        public int id { get; set; }
        public string description { get; set; }
        public int base_qty { get; set; }
        public string base_unit { get; set; }
        public int category_id { get; set; }
        public Attributes attributes { get; set; }
    }
}
