using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace Day_07
{
    class Program
    {
        static void Main(string[] args)
        {
            string[] input = File.ReadAllLines("input.txt");

            VirtualFolder _root = new VirtualFolder(null, "/");
            VirtualFolder _current = _root;

            foreach (var l in input)
            {
                //Command
                if (l[0] == '$')
                {
                    if (l.StartsWith("$ cd"))
                    {
                        //Move _current
                        string target = l.Substring(5);

                        if (target == "/")
                        {
                            _current = _root;
                        }
                        else if (target == "..")
                        {
                            if (_current.Parent != null)
                            {
                                _current = (VirtualFolder)_current.Parent;
                            }
                        }
                        else
                        {
                            _current = (VirtualFolder)_current.GetByName(target);
                        }
                    }
                }
                //Add dir
                else if (l[0] == 'd')
                {
                    _current.AddObject(new VirtualFolder(_current, l.Substring(4), null));
                }
                //Add file
                else
                {
                    string[] data = l.Split(new char[] { ' ' });
                    _current.AddObject(new VirtualFile(_current, data[1], int.Parse(data[0])));
                }

            }

            int sum_sub100k = 0;
            int size;
            foreach (var d in _root.AllSubFolders())
            {
                size = d.Size();

                if (size <= 100000)
                {
                    sum_sub100k += size;
                }
            }

            Console.WriteLine($"Sum Sub-100k dirs: {sum_sub100k}");

            List<VirtualFolder> allFolders = _root.AllSubFolders();
            allFolders.Sort((a, b) => a.Size().CompareTo(b.Size()));
            int toDeleteSize = _root.Size() - (70000000 - 30000000);

            foreach(var vf in allFolders)
            {
                if(vf.Size()>= toDeleteSize)
                {
                    Console.WriteLine($"\nUsage: {_root.Size()}/70000000\nTo delete: {toDeleteSize}\nDelete: {vf.Name} ({vf.Size()})");
                    break;
                }
            }

            Console.ReadLine();
        }
    }

    interface IVirtualFileSystemObject
    {
        string Name { get; }

        IVirtualFileSystemObject Parent { get; }

        int Size();
    }

    class VirtualFile : IVirtualFileSystemObject
    {
        private string _name;
        private int _size;

        private IVirtualFileSystemObject _parent;

        public VirtualFile(VirtualFolder parent, string name, int size)
        {
            _parent = parent;
            _name = name;
            _size = size;
        }

        public IVirtualFileSystemObject Parent
        {
            get
            {
                return _parent;
            }
        }

        public string Name
        {
            get
            {
                return _name;
            }
        }

        public int Size()
        {
            return _size;
        }
    }

    class VirtualFolder : IVirtualFileSystemObject
    {
        private string _name;
        private VirtualFolder _parent;
        private Dictionary<string, IVirtualFileSystemObject> _subObjects;

        public VirtualFolder(VirtualFolder parent, string name) : this(parent, name, null) { }
        public VirtualFolder(VirtualFolder parent, string name, IEnumerable<IVirtualFileSystemObject> fsos)
        {
            _parent = parent;
            _name = name;

            if (fsos != null)
            {
                foreach (var o in fsos)
                {
                    _subObjects.Add(o.Name, o);
                }
            }
            else
            {
                _subObjects = new Dictionary<string, IVirtualFileSystemObject>();
            }
        }

        public IVirtualFileSystemObject Parent
        {
            get
            {
                return _parent;
            }
        }

        public string Name
        {
            get
            {
                return _name;
            }
        }

        public int Size()
        {
            int size = 0;

            foreach (var fso in _subObjects.Values)
            {
                size += fso.Size();
            }

            return size;
        }

        public void AddObject(IVirtualFileSystemObject fso)
        {
            _subObjects.Add(fso.Name, fso);
        }

        public void RemoveObject(IVirtualFileSystemObject fso)
        {
            _subObjects.Remove(fso.Name);
        }

        public void RemoveObject(string name)
        {
            _subObjects.Remove(name);
        }

        public IVirtualFileSystemObject GetByName(string name)
        {
            return _subObjects[name];
        }

        public List<VirtualFolder> AllSubFolders()
        {
            List<VirtualFolder> _ret = new List<VirtualFolder>();

            foreach (var fso in _subObjects.Values)
            {
                if (fso is VirtualFolder)
                {
                    VirtualFolder vf = (VirtualFolder)fso;
                    _ret.Add((VirtualFolder)fso);
                    _ret.AddRange(((VirtualFolder)fso).AllSubFolders());
                }
            }

            return _ret;
        }

    }

}
