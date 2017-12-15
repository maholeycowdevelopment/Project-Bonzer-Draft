using java.io;
using edu.stanford.nlp.process;
using edu.stanford.nlp.ling;
using edu.stanford.nlp.trees;
using edu.stanford.nlp.parser.lexparser;
using Console = System.Console;

namespace RoughDraftBonzer
{
    class LinguisticAnalyzer
    { 
        public void ProcessText(string inputText)
        {
            var jarRoot = "C:\\stanford-parser-full-2016-10-31\\stanford-parser-3.7.0-models";//\\edu\\stanford\\nlp\\models";//"nlp.stanford.edu\\stanford-parser-full-2017-06-09\\models";
            var modelsDirectory = jarRoot + "\\edu\\stanford\\nlp\\models";

            // Loading english PCFG parser from file
            var lp = LexicalizedParser.loadModel(modelsDirectory + "\\lexparser\\englishPCFG.ser.gz");

            // This option shows loading and using an explicit tokenizer
            var tokenizerFactory = PTBTokenizer.factory(new CoreLabelTokenFactory(), "");
            var sentReader = new StringReader(inputText);
            var rawWords = tokenizerFactory.getTokenizer(sentReader).tokenize();
            sentReader.close();
            var tree = lp.apply(rawWords);

            //Extract dependencies from lexical tree
            var tlp = new PennTreebankLanguagePack();
            var gsf = tlp.grammaticalStructureFactory();
            var gs = gsf.newGrammaticalStructure(tree);
            var tdl = gs.typedDependenciesCCprocessed();
            Console.WriteLine("\n{0}\n", tdl);

            // Extract collapsed dependencies from parsed tree
            var tp = new TreePrint("penn,typedDependenciesCollapsed");
            tp.printTree(tree);
        }
    }
}
