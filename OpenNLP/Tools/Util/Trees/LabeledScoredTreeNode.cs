﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenNLP.Tools.Util.Ling;

namespace OpenNLP.Tools.Util.Trees
{
    public class LabeledScoredTreeNode : Tree
    {
        private static readonly long serialVersionUID = -8992385140984593817L;

        /**
   * Label of the parse tree.
   */
        private Label p_label; // = null;

        /**
   * Score of <code>TreeNode</code>
   */
        private double p_score = double.NaN;

        /**
   * Daughters of the parse tree.
   */
        private Tree[] daughterTrees; // = null;

        /**
   * Create an empty parse tree.
   */

        public LabeledScoredTreeNode()
        {
            SetChildren(EMPTY_TREE_ARRAY);
        }

        /**
   * Create a leaf parse tree with given word.
   *
   * @param label the <code>Label</code> representing the <i>word</i> for
   *              this new tree leaf.
   */

        public LabeledScoredTreeNode(Label label) : this(label, Double.NaN)
        {
        }

        /**
   * Create a leaf parse tree with given word and score.
   *
   * @param label The <code>Label</code> representing the <i>word</i> for
   * @param score The score for the node
   *              this new tree leaf.
   */

        public LabeledScoredTreeNode(Label label, double score) : this()
        {
            this.p_label = label;
            this.p_score = score;
        }

        /**
   * Create parse tree with given root and array of daughter trees.
   *
   * @param label             root label of tree to construct.
   * @param daughterTreesList List of daughter trees to construct.
   */

        public LabeledScoredTreeNode(Label label, List<Tree> daughterTreesList)
        {
            this.p_label = label;
            SetChildren(daughterTreesList);
        }

        /**
   * Returns an array of children for the current node, or null
   * if it is a leaf.
   */
        //@Override
        public override Tree[] Children()
        {
            return daughterTrees;
        }

        /**
   * Sets the children of this <code>Tree</code>.  If given
   * <code>null</code>, this method sets the Tree's children to
   * the canonical zero-length Tree[] array.
   *
   * @param children An array of child trees
   */
        //@Override
        public override void SetChildren(Tree[] children)
        {
            if (children == null)
            {
                daughterTrees = EMPTY_TREE_ARRAY;
            }
            else
            {
                daughterTrees = children;
            }
        }

        /**
   * Returns the label associated with the current node, or null
   * if there is no label
   */
        //@Override
        public override Label Label()
        {
            return p_label;
        }

        /**
   * Sets the label associated with the current node, if there is one.
   */
        //@Override
        public override void SetLabel( /*final */ Label label)
        {
            this.p_label = label;
        }

        /**
   * Returns the score associated with the current node, or Nan
   * if there is no score
   */
        //@Override
        public override double Score()
        {
            return p_score;
        }

        /**
   * Sets the score associated with the current node, if there is one
   */
        //@Override
        public override void SetScore(double score)
        {
            this.p_score = score;
        }

        /**
   * Return a <code>TreeFactory</code> that produces trees of the
   * same type as the current <code>Tree</code>.  That is, this
   * implementation, will produce trees of type
   * <code>LabeledScoredTree(Node|Leaf)</code>.
   * The <code>Label</code> of <code>this</code>
   * is examined, and providing it is not <code>null</code>, a
   * <code>LabelFactory</code> which will produce that kind of
   * <code>Label</code> is supplied to the <code>TreeFactory</code>.
   * If the <code>Label</code> is <code>null</code>, a
   * <code>StringLabelFactory</code> will be used.
   * The factories returned on different calls a different: a new one is
   * allocated each time.
   *
   * @return a factory to produce labeled, scored trees
   */
        //@Override
        public override TreeFactory TreeFactory()
        {
            LabelFactory lf = (Label() == null) ? CoreLabel.Factory() : Label().LabelFactory();
            return new LabeledScoredTreeFactory(lf);
        }

        // extra class guarantees correct lazy loading (Bloch p.194)
        private static class TreeFactoryHolder
        {
            public static readonly TreeFactory tf = new LabeledScoredTreeFactory();
        }

        /**
   * Return a <code>TreeFactory</code> that produces trees of the
   * <code>LabeledScoredTree{Node|Leaf}</code> type.
   * The factory returned is always the same one (a singleton).
   *
   * @return a factory to produce labeled, scored trees
   */

        public static TreeFactory Factory()
        {
            return TreeFactoryHolder.tf;
        }

        /**
   * Return a <code>TreeFactory</code> that produces trees of the
   * <code>LabeledScoredTree{Node|Leaf}</code> type, with
   * the <code>Label</code> made with the supplied
   * <code>LabelFactory</code>.
   * The factory returned is a different one each time
   *
   * @param lf The LabelFactory to use
   * @return a factory to produce labeled, scored trees
   */

        public static TreeFactory Factory(LabelFactory lf)
        {
            return new LabeledScoredTreeFactory(lf);
        }

        private static readonly string nf = "#.###";

        //@Override
        public override string NodeString()
        {
            var buff = new StringBuilder();
            buff.Append(base.NodeString());
            if (! double.IsNaN(p_score))
            {
                buff.Append(" [").Append((-p_score).ToString(nf)).Append("]");
            }
            return buff.ToString();
        }
    }
}