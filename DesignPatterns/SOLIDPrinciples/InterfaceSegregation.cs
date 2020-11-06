namespace SOLIDPrinciples
{
    /// Interface segregation principle:
    /// In large applications, interfaces shall be segregated so that
    /// no one who implements an interface implements functions that they dont actually need
    ///
    /// For example: if you have an interface for machines with methods to print(), scan(), and fax(),
    /// one class implementing that interface may be suitable to implement all of these methods, but
    /// if a printer that does not have scan or fax functionality, it would have to implement methods it does not
    /// need. Instead the interfaces should be more atomic and self contained. 
    
    public class Document
    {
        //document data...
    }

    public interface IPrinter
    {
        void Print(Document document);
    }

    public interface IScanner
    {
        void Scan(Document document);
    }

    public interface IFaxer
    {
        void Fax(Document document);
    }

    /// <summary>
    /// Implements IPrinter, IScanner, and IFaxer
    /// </summary>
    public class PhotoCopier : IPrinter, IScanner, IFaxer
    {
        public void Print(Document document)
        {
            //printing logic
        }

        public void Scan(Document document)
        {
            //scanning logic
        }

        public void Fax(Document document)
        {
            //faxing logic
        }
    }

    /// <summary>
    /// Implements only the IPrinter interface
    /// </summary>
    public class OldPrinter : IPrinter
    {
        public void Print(Document document)
        {
            //printing logic
        }
    }
    
    
    /// <summary>
    /// Combines the interfaces into a single interface through inheritance
    /// </summary>
    public interface IMultiFunctionDevice : IScanner, IPrinter //... and more intefaces if needed
    {
        
    }

    /// <summary>
    /// A multipurpose class that implements IMultiFunctionalDevice using the Delegator pattern.
    /// A reference to an object for each interface is kept by an object,
    /// and the MultiFunctionMachine delegates method calls to those objects.
    /// </summary>
    public class MultiFunctionMachine : IMultiFunctionDevice
    {
        //Variables for the interfaces that will be delegated to. 
        private IScanner scanner;
        private IPrinter printer;

        public MultiFunctionMachine(IScanner scanner, IPrinter printer)
        {
            this.scanner = scanner;
            this.printer = printer;
        }

        //delegate scanning
        public void Scan(Document document)
        {
            scanner.Scan(document);
        }

        //delegate print
        public void Print(Document document)
        {
            printer.Print(document);
        }
    }
}