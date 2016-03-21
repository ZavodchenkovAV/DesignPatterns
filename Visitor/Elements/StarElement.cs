﻿using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Visitor.Visitors;

namespace Visitor.Elements
{
    public class StarElement:BaseElement
    {
        protected StarElement(Rectangle rect)
            :base(rect)
        {

        }
        public override void Accept(BaseVisitor visitor)
        {
            visitor.Visit(this);
        }
    }
}