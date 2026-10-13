using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ImportFileParser;
using System.Collections.Generic;
using ImportFileParser.RowObjects;
using ImportFileParser.Common;

namespace ImportParserTests
{
    [TestClass]
    public class ExpressionBuilderTest
    {
        CRule m_TestingRule = null;
        CTransRec m_TransRec = new CTransRec("SBR", "ALF", DateTime.Now, 10, 10, "some description", Constants.CONST_CATEGORY_INT, "8999");

        [TestMethod]
        public void TestAndExpression()
        {
            m_TestingRule = new CRule("EQ(Category, 'INT') and LIKE(Desc, 'cript')", "TST");

            if (!m_TestingRule.ApplyRule(m_TransRec))
            {
                Assert.Fail("TestAnd_Match_NotWork");
            }

            if (m_TransRec.Category != "TST")
            {
                Assert.Fail("TestAnd_Catg_NotSetup");
            }

            m_TestingRule = new CRule("EQ(Category, 'ENT') and LIKE(Desc, 'cript')", Constants.CONST_CATEGORY_INT);

            if (m_TestingRule.ApplyRule(m_TransRec))
            {
                Assert.Fail("TestAnd_NotMatch_NotWork");
            }

            if (m_TransRec.Category == Constants.CONST_CATEGORY_INT)
            {
                Assert.Fail("TestAnd_Catg_Setup");
            }

            m_TransRec.Category = Constants.CONST_CATEGORY_INT;
            m_TestingRule = null;
        }

        [TestMethod]
        public void TestOrExpression()
        {
            m_TestingRule = new CRule("EQ(Category, 'ENT') or LIKE(Desc, 'cript')", "TST");

            if (!m_TestingRule.ApplyRule(m_TransRec))
            {
                Assert.Fail("TestOr_Match_NotWork");
            }

            if (m_TransRec.Category != "TST")
            {
                Assert.Fail("TestOr_Catg_NotSetup");
            }

            m_TestingRule = new CRule("EQ(Category, 'ENT') or LIKE(Desc, 'not matching')", Constants.CONST_CATEGORY_INT);

            if (m_TestingRule.ApplyRule(m_TransRec))
            {
                Assert.Fail("TestOr_NotMatch_NotWork");
            }

            if (m_TransRec.Category == Constants.CONST_CATEGORY_INT)
            {
                Assert.Fail("TestOr_Catg_Setup");
            }

            m_TransRec.Category = Constants.CONST_CATEGORY_INT;
            m_TestingRule = null;
        }

        [TestMethod]
        public void TestSimpleLikeExpression()
        {
            m_TestingRule = new CRule("LIKE(Desc, 'cript')", "TST");

            if (!m_TestingRule.ApplyRule(m_TransRec))
            {
                Assert.Fail("TestLike_Match_NotWork");
            }

            if (m_TransRec.Category != "TST")
            {
                Assert.Fail("TestLike_Catg_NotSetup");
            }

            m_TestingRule = new CRule("LIKE(Desc, 'not match')", Constants.CONST_CATEGORY_INT);

            if (m_TestingRule.ApplyRule(m_TransRec))
            {
                Assert.Fail("TestLike_NotMatch_NotWork");
            }

            if (m_TransRec.Category == Constants.CONST_CATEGORY_INT)
            {
                Assert.Fail("TestLike_Catg_Setup");
            }

            m_TransRec.Category = Constants.CONST_CATEGORY_INT;
            m_TestingRule = null;
        }

        [TestMethod]
        public void TestSimpleLikeNullExpression()
        {
            m_TransRec.Category = null;
            m_TestingRule = new CRule("LIKE(Category, 'cript')", "TST");

            if (m_TestingRule.ApplyRule(m_TransRec))
            {
                Assert.Fail("TestLike_Match_NotWork");
            }           

            if (m_TransRec.Category == "TST")
            {
                Assert.Fail("TestLike_Catg_Setup");
            }

            m_TransRec.Category = Constants.CONST_CATEGORY_INT;
            m_TestingRule = null;
        }

        [TestMethod]
        public void TestSimpleEQExpression()
        {
            m_TestingRule = new CRule("EQ(Category, 'INT')", "TST");

            if (!m_TestingRule.ApplyRule(m_TransRec))
            {
                Assert.Fail("TestEQ_Match_NotWork");
            }

            if (m_TransRec.Category != "TST")
            {
                Assert.Fail("TestEQ_Catg_NotSetup");
            }

            m_TestingRule = new CRule("EQ(Category, 'INT')", "ENT");

            if (m_TestingRule.ApplyRule(m_TransRec))
            {
                Assert.Fail("TestEQ_NotMatch_NotWork");
            }

            if (m_TransRec.Category == "ENT")
            {
                Assert.Fail("TestEQ_Catg_Setup");
            }

            m_TransRec.Category = Constants.CONST_CATEGORY_INT;
            m_TestingRule = null;
        }

        [TestMethod]
        public void TestSimpleISNULLExpression()
        {
            m_TransRec.FromAccount = Constants.CONST_EMPTY_VALUE;
            m_TestingRule = new CRule("ISNULL(FromAcc)", "TST");

            if (!m_TestingRule.ApplyRule(m_TransRec))
            {
                Assert.Fail("TestIsNull_Match_NotWork");
            }

            if (m_TransRec.Category != "TST")
            {
                Assert.Fail("TestIsNull_Catg_NotSetup");
            }

            m_TransRec.FromAccount = "SBR";

            m_TestingRule = new CRule("ISNULL(Catg)", Constants.CONST_CATEGORY_INT);

            if (m_TestingRule.ApplyRule(m_TransRec))
            {
                Assert.Fail("TestIsNull_NotMatch_NotWork");
            }

            if (m_TransRec.Category == Constants.CONST_CATEGORY_INT)
            {
                Assert.Fail("TestIsNull_Catg_Setup");
            }

            m_TransRec.Category = Constants.CONST_CATEGORY_INT;
            m_TestingRule = null;
        }

        [TestMethod]
        public void TestSimpleISNULLExpression1()
        {
            string v_OldDesc = m_TransRec.Description;
            m_TransRec.Description = string.Empty;
            m_TestingRule = new CRule("ISNULL(Desc)", "TST");

            if (!m_TestingRule.ApplyRule(m_TransRec))
            {
                Assert.Fail("TestIsNull1_Match_NotWork");
            }

            if (m_TransRec.Category != "TST")
            {
                Assert.Fail("TestIsNull1_Catg_NotSetup");
            }

            m_TransRec.Description = v_OldDesc;

            m_TestingRule = new CRule("ISNULL(Desc)", Constants.CONST_CATEGORY_INT);

            if (m_TestingRule.ApplyRule(m_TransRec))
            {
                Assert.Fail("TestIsNull1_NotMatch_NotWork");
            }

            if (m_TransRec.Category == Constants.CONST_CATEGORY_INT)
            {
                Assert.Fail("TestIsNull1_Catg_Setup");
            }

            m_TransRec.Category = Constants.CONST_CATEGORY_INT;
            m_TestingRule = null;
        }

        [TestMethod]
        public void TestSimpleISNULLExpression2()
        {
            string v_OldDesc = m_TransRec.Description;
            m_TransRec.Description = null;
            m_TestingRule = new CRule("ISNULL(Desc)", "TST");

            if (!m_TestingRule.ApplyRule(m_TransRec))
            {
                Assert.Fail("TestIsNull2_Match_NotWork");
            }

            if (m_TransRec.Category != "TST")
            {
                Assert.Fail("TestIsNull2_Catg_NotSetup");
            }

            m_TransRec.Description = v_OldDesc;

            m_TestingRule = new CRule("ISNULL(Desc)", Constants.CONST_CATEGORY_INT);

            if (m_TestingRule.ApplyRule(m_TransRec))
            {
                Assert.Fail("TestIsNull2_NotMatch_NotWork");
            }

            if (m_TransRec.Category == Constants.CONST_CATEGORY_INT)
            {
                Assert.Fail("TestIsNull2_Catg_Setup");
            }

            m_TransRec.Category = Constants.CONST_CATEGORY_INT;
            m_TestingRule = null;
        }
    }
}