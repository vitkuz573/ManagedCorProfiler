using System.Collections;
using System.Collections.Frozen;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using Windows.Win32.System.Com;
using Windows.Win32.System.Diagnostics.ClrProfiling;

namespace Windows.Win32
{
    static unsafe partial class ComHelpers
    {
        static partial void PopulateIUnknownImpl<TComInterface>(IUnknown.Vtbl* vtable) where TComInterface : unmanaged
        {
            Console.WriteLine($"PopulateIUnknownImpl -> {typeof(TComInterface)}");

            // Get system provided IUnknown implementation.
            ComWrappers.GetIUnknownImpl(
                out nint fpQueryInterface,
                out nint fpAddRef,
                out nint fpRelease);

            // IUnknown
            vtable->QueryInterface_1 = (delegate* unmanaged[Stdcall]<IUnknown*, Guid*, void**, Foundation.HRESULT>)fpQueryInterface;
            vtable->AddRef_2 = (delegate* unmanaged[Stdcall]<IUnknown*, uint>)fpAddRef;
            vtable->Release_3 = (delegate* unmanaged[Stdcall]<IUnknown*, uint>)fpRelease;
        }
    }
}

namespace ClrProfiling.ComInterop.Wrappers
{
    internal unsafe class CorProfilerComWrappers : ComWrappers
    {
        readonly delegate*<nint, object?> _createIfSupported;

        public CorProfilerComWrappers()
        {
            _createIfSupported = &CorProfilerDynamicWrapper.CreateIfSupported;

            Console.WriteLine("CTOR CorProfilerComWrappers");
        }

        private static nint GetICorProfilerCallbackVTable()
        {
            return (nint)CreateICorProfilerCallbackVTable();
        }

        private static nint GetICorProfilerCallback2VTable()
        {
            return (nint)CreateICorProfilerCallback2VTable();
        }

        private static nint GetICorProfilerCallback3VTable()
        {
            return (nint)CreateICorProfilerCallback3VTable();
        }

        private static nint GetICorProfilerCallback4VTable()
        {
            return (nint)CreateICorProfilerCallback4VTable();
        }

        private static nint GetICorProfilerCallback5VTable()
        {
            return (nint)CreateICorProfilerCallback5VTable();
        }

        private static nint GetICorProfilerCallback6VTable()
        {
            return (nint)CreateICorProfilerCallback6VTable();
        }

        private static nint GetICorProfilerCallback7VTable()
        {
            return (nint)CreateICorProfilerCallback7VTable();
        }

        private static nint GetICorProfilerCallback8VTable()
        {
            return (nint)CreateICorProfilerCallback8VTable();
        }

        private static nint GetICorProfilerCallback9VTable()
        {
            return (nint)CreateICorProfilerCallback9VTable();
        }

        // Перемещаем вспомогательные методы на уровень класса и объявляем их как private static
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static ICorProfilerCallback.Vtbl* CreateICorProfilerCallbackVTable()
        {
            // Реализуйте логику создания VTable для ICorProfilerCallback
            throw new NotImplementedException("Реализуйте логику создания VTable для ICorProfilerCallback.");
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static ICorProfilerCallback2.Vtbl* CreateICorProfilerCallback2VTable()
        {
            // Реализуйте логику создания VTable для ICorProfilerCallback2
            throw new NotImplementedException("Реализуйте логику создания VTable для ICorProfilerCallback2.");
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static ICorProfilerCallback3.Vtbl* CreateICorProfilerCallback3VTable()
        {
            // Реализуйте логику создания VTable для ICorProfilerCallback3
            throw new NotImplementedException("Реализуйте логику создания VTable для ICorProfilerCallback3.");
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static ICorProfilerCallback4.Vtbl* CreateICorProfilerCallback4VTable()
        {
            // Реализуйте логику создания VTable для ICorProfilerCallback4
            throw new NotImplementedException("Реализуйте логику создания VTable для ICorProfilerCallback4.");
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static ICorProfilerCallback5.Vtbl* CreateICorProfilerCallback5VTable()
        {
            // Реализуйте логику создания VTable для ICorProfilerCallback5
            throw new NotImplementedException("Реализуйте логику создания VTable для ICorProfilerCallback5.");
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static ICorProfilerCallback6.Vtbl* CreateICorProfilerCallback6VTable()
        {
            // Реализуйте логику создания VTable для ICorProfilerCallback6
            throw new NotImplementedException("Реализуйте логику создания VTable для ICorProfilerCallback6.");
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static ICorProfilerCallback7.Vtbl* CreateICorProfilerCallback7VTable()
        {
            // Реализуйте логику создания VTable для ICorProfilerCallback7
            throw new NotImplementedException("Реализуйте логику создания VTable для ICorProfilerCallback7.");
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static ICorProfilerCallback8.Vtbl* CreateICorProfilerCallback8VTable()
        {
            // Реализуйте логику создания VTable для ICorProfilerCallback8
            throw new NotImplementedException("Реализуйте логику создания VTable для ICorProfilerCallback8.");
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static ICorProfilerCallback9.Vtbl* CreateICorProfilerCallback9VTable()
        {
            // Реализуйте логику создания VTable для ICorProfilerCallback9
            throw new NotImplementedException("Реализуйте логику создания VTable для ICorProfilerCallback9.");
        }

        private readonly static FrozenDictionary<Type, Func<nint>> ICorProfilerCallbackVTableFactoryMap = new Dictionary<Type, Func<nint>>
        {
            [typeof(ICorProfilerCallback.Interface)] = GetICorProfilerCallbackVTable,
            [typeof(ICorProfilerCallback2.Interface)] = GetICorProfilerCallback2VTable,
            [typeof(ICorProfilerCallback3.Interface)] = GetICorProfilerCallback3VTable,
            [typeof(ICorProfilerCallback4.Interface)] = GetICorProfilerCallback4VTable,
            [typeof(ICorProfilerCallback5.Interface)] = GetICorProfilerCallback5VTable,
            [typeof(ICorProfilerCallback6.Interface)] = GetICorProfilerCallback6VTable,
            [typeof(ICorProfilerCallback7.Interface)] = GetICorProfilerCallback7VTable,
            [typeof(ICorProfilerCallback8.Interface)] = GetICorProfilerCallback8VTable,
            [typeof(ICorProfilerCallback9.Interface)] = GetICorProfilerCallback9VTable,
        }.ToFrozenDictionary();

        private readonly static FrozenDictionary<Type, Guid> ICorProfilerCallbackIIDMap = new Dictionary<Type, Guid>
        {
            [typeof(ICorProfilerCallback.Interface)] = ICorProfilerCallback.IID_Guid,
            [typeof(ICorProfilerCallback2.Interface)] = ICorProfilerCallback2.IID_Guid,
            [typeof(ICorProfilerCallback3.Interface)] = ICorProfilerCallback3.IID_Guid,
            [typeof(ICorProfilerCallback4.Interface)] = ICorProfilerCallback4.IID_Guid,
            [typeof(ICorProfilerCallback5.Interface)] = ICorProfilerCallback5.IID_Guid,
            [typeof(ICorProfilerCallback6.Interface)] = ICorProfilerCallback6.IID_Guid,
            [typeof(ICorProfilerCallback7.Interface)] = ICorProfilerCallback7.IID_Guid,
            [typeof(ICorProfilerCallback8.Interface)] = ICorProfilerCallback8.IID_Guid,
            [typeof(ICorProfilerCallback9.Interface)] = ICorProfilerCallback9.IID_Guid,
        }.ToFrozenDictionary();

        private const string ICorProfilerCallbackPrefix = "ICorProfilerCallback";
        private const string Interface = "Interface";

        private readonly static FrozenSet<Type> ICorProfilerCallbackInterfaces = new HashSet<Type>
        {
            typeof(ICorProfilerCallback.Interface),
            typeof(ICorProfilerCallback2.Interface),
            typeof(ICorProfilerCallback3.Interface),
            typeof(ICorProfilerCallback4.Interface),
            typeof(ICorProfilerCallback5.Interface),
            typeof(ICorProfilerCallback6.Interface),
            typeof(ICorProfilerCallback7.Interface),
            typeof(ICorProfilerCallback8.Interface),
            typeof(ICorProfilerCallback9.Interface),
        }.ToFrozenSet();

        protected override unsafe ComInterfaceEntry* ComputeVtables(object obj, CreateComInterfaceFlags flags, out int count)
        {
            Console.WriteLine($"CALL ComputeVtables {obj.GetType()}");

            Debug.Assert(flags is CreateComInterfaceFlags.None);

            var profilerCallbackIfaces = obj
                .GetType()
                .GetInterfaces()
                .Where(ICorProfilerCallbackInterfaces.Contains)
                .ToArray();

            Console.WriteLine($"Found {profilerCallbackIfaces.Length} ICorProfilerCallbackX interfaces.");

            foreach (var i in profilerCallbackIfaces)
            {
                Console.WriteLine($"IFACE {i.FullName}");
            }

            if (profilerCallbackIfaces.Length == 0)
            {
                throw new InvalidOperationException($"no ICorProfilerCallbackX interface found for type '{obj.GetType().FullName}'");
            }

            var implDefinitionLen = profilerCallbackIfaces.Length;

            Console.WriteLine($"Creating ComInterfaceEntry for {implDefinitionLen} implementations.");

            var implDef = (ComInterfaceEntry*)RuntimeHelpers.AllocateTypeAssociatedMemory(
                    typeof(CorProfilerComWrappers),
                    sizeof(ComInterfaceEntry) * implDefinitionLen);

            var idx = 0;

            for (var i = 0; i < implDefinitionLen; i++)
            {
                var iface = profilerCallbackIfaces[i];

                Console.WriteLine($"INIT impl {i} idx={idx} iface={iface.FullName}");

                implDef[idx].IID = ICorProfilerCallbackIIDMap[iface];

                Console.WriteLine($"INIT impl IID {iface.FullName} -> {implDef[idx].IID}");

                try
                {
                    implDef[idx].Vtable = ICorProfilerCallbackVTableFactoryMap[iface]();
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Failed to populate the Vtable: {ex}");
                    throw;
                }

                Console.WriteLine($"INIT impl VTABLE {iface.FullName} -> 0x{(ulong)implDef[idx].Vtable:x8}");
                idx++;
            }

            count = implDefinitionLen;

            return implDef;
        }

        protected override object? CreateObject(nint externalComObject, CreateObjectFlags flags)
        {
            Console.WriteLine($"CALL CreateObject 0x{externalComObject:x8}");

            Debug.Assert(flags.HasFlag(CreateObjectFlags.UniqueInstance));

            // Throw an exception if the type is not supported by the implementation.
            // Null can be returned as well, but an ArgumentNullException will be thrown for
            // the consumer of this ComWrappers instance.
            return _createIfSupported(externalComObject) ?? throw new NotSupportedException();
        }

        protected override void ReleaseObjects(IEnumerable objects)
        {
            Console.WriteLine($"THROW ReleaseObjects");
            throw new NotImplementedException();
        }
    }
}
