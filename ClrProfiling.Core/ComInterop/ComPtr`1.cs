﻿// Copyright © Tanner Gooding and Contributors. Licensed under the MIT License (MIT). See License.md in the repository root for more information.

// Ported from winrt/wrl/client.h in the Windows SDK for Windows 10.0.22621.0
// Original source is Copyright © Microsoft. All rights reserved.

using System.Diagnostics;
using System.Runtime.CompilerServices;
using Windows.Win32.Foundation;

namespace Windows.Win32.System.Com;

/// <summary>A type that allows working with pointers to COM objects more securely.</summary>
/// <typeparam name="T">The type to wrap in the current <see cref="ComPtr{T}"/> instance.</typeparam>
/// <remarks>
/// While this type is not marked as <see langword="ref"/> so that it can also be used in fields, make sure to keep the reference counts properly tracked if you do store <see cref="ComPtr{T}"/> instances on the heap.
/// NOTE: due to COM objects being structs (generated by cswin32 generator), part of the safety is lost and the pointer wrapped by a <see cref="ComPtr{T}"/> instance
/// is assumed (and NOT checked) to be castable to IUnknown.
/// </remarks>
public unsafe struct ComPtr<T>: IDisposable where T : unmanaged, IComIID
{
    /// <summary>The raw pointer to a COM object, if existing.</summary>
    private IUnknown* _ptr;

    public ComPtr()
    {
        _ptr = null;
    }

    /// <summary>Creates a new <see cref="ComPtr{T}"/> instance from a raw pointer and increments the ref count.</summary>
    /// <param name="other">The raw pointer to wrap.</param>
    public ComPtr(T* other)
    {
        _ptr = (IUnknown*)other;
        InternalAddRef();
    }

    /// <summary>Creates a new <see cref="ComPtr{T}"/> instance from a second one and increments the ref count.</summary>
    /// <param name="other">The other <see cref="ComPtr{T}"/> instance to copy.</param>
    public ComPtr(ComPtr<T> other)
    {
        _ptr = other._ptr;
        InternalAddRef();
    }

    /// <summary>Converts a raw pointer to a new <see cref="ComPtr{T}"/> instance and increments the ref count.</summary>
    /// <param name="other">The raw pointer to wrap.</param>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static implicit operator ComPtr<T>(T* other) => new(other);

    /// <summary>Unwraps a <see cref="ComPtr{T}"/> instance and returns the internal raw pointer.</summary>
    /// <param name="other">The <see cref="ComPtr{T}"/> instance to unwrap.</param>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static implicit operator T*(ComPtr<T> other) => other.Get();

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static implicit operator IUnknown**(ComPtr<T> other) => &other._ptr;

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private readonly Guid* __uuidof<U>() where U : unmanaged, IComIID
    {
        return (Guid*)Unsafe.AsPointer(ref Unsafe.AsRef(in U.Guid));
    }

    /// <summary>Converts the current object reference to type <typeparamref name="U"/> and assigns that to a target <see cref="ComPtr{T}"/> value.</summary>
    /// <typeparam name="U">The interface type to use to try casting the current COM object.</typeparam>
    /// <param name="p">A raw pointer to the target <see cref="ComPtr{T}"/> value to write to.</param>
    /// <returns>The result of <see cref="IUnknown.QueryInterface"/> for the target type <typeparamref name="U"/>.</returns>
    /// <remarks>This method will automatically release the target COM object pointed to by <paramref name="p"/>, if any.</remarks>
    public readonly HRESULT As<U>(ComPtr<U>* p) where U : unmanaged, IComIID
    {
        var iid = (Guid*)Unsafe.AsPointer(ref Unsafe.AsRef(in U.Guid));

        return _ptr->QueryInterface(__uuidof<U>(), (void**)p->ReleaseAndGetAddressOf());
    }

    /// <summary>Converts the current object reference to type <typeparamref name="U"/> and assigns that to a target <see cref="ComPtr{T}"/> value.</summary>
    /// <typeparam name="U">The interface type to use to try casting the current COM object.</typeparam>
    /// <param name="other">A reference to the target <see cref="ComPtr{T}"/> value to write to.</param>
    /// <returns>The result of <see cref="IUnknown.QueryInterface"/> for the target type <typeparamref name="U"/>.</returns>
    /// <remarks>This method will automatically release the target COM object pointed to by <paramref name="other"/>, if any.</remarks>
    public readonly HRESULT As<U>(ref ComPtr<U> other) where U : unmanaged, IComIID
    {
        U* ptr;
        HRESULT result = _ptr->QueryInterface(__uuidof<U>(), (void**)&ptr);

        other.Attach(ptr);

        return result;
    }

    /// <summary>Converts the current object reference to a type indicated by the given IID and assigns that to a target <see cref="ComPtr{T}"/> value.</summary>
    /// <param name="riid">The IID indicating the interface type to convert the COM object reference to.</param>
    /// <param name="other">A raw pointer to the target <see cref="ComPtr{T}"/> value to write to.</param>
    /// <returns>The result of <see cref="IUnknown.QueryInterface"/> for the target IID.</returns>
    /// <remarks>This method will automatically release the target COM object pointed to by <paramref name="other"/>, if any.</remarks>
    public readonly HRESULT AsIID(Guid* riid, ComPtr<IUnknown>* other)
    {
        return _ptr->QueryInterface(riid, (void**)other->ReleaseAndGetAddressOf());
    }

    /// <summary>Converts the current object reference to a type indicated by the given IID and assigns that to a target <see cref="ComPtr{T}"/> value.</summary>
    /// <param name="riid">The IID indicating the interface type to convert the COM object reference to.</param>
    /// <param name="other">A reference to the target <see cref="ComPtr{T}"/> value to write to.</param>
    /// <returns>The result of <see cref="IUnknown.QueryInterface"/> for the target IID.</returns>
    /// <remarks>This method will automatically release the target COM object pointed to by <paramref name="other"/>, if any.</remarks>
    public readonly HRESULT AsIID(Guid* riid, ref ComPtr<IUnknown> other)
    {
        IUnknown* ptr;
        HRESULT result = _ptr->QueryInterface(riid, (void**)&ptr);

        other.Attach(ptr);

        return result;
    }

    /// <summary>Releases the current COM object, if any, and replaces the internal pointer with an input raw pointer.</summary>
    /// <param name="other">The input raw pointer to wrap.</param>
    /// <remarks>This method will release the current raw pointer, if any, but it will not increment the references for <paramref name="other"/>.</remarks>
    public void Attach(T* other)
    {
        if (_ptr != null)
        {
            var @ref = _ptr->Release();
            Debug.Assert((@ref != 0) || (_ptr != other));
        }

        _ptr = (IUnknown*)other;
    }

    /// <summary>Returns the raw pointer wrapped by the current instance, and resets the current <see cref="ComPtr{T}"/> value.</summary>
    /// <returns>The raw pointer wrapped by the current <see cref="ComPtr{T}"/> value.</returns>
    /// <remarks>This method will not change the reference count for the COM object in use.</remarks>
    public T* Detach()
    {
        T* ptr = (T*)_ptr;
        _ptr = null;

        return ptr;
    }

    /// <summary>Increments the reference count for the current COM object, if any, and copies its address to a target raw pointer.</summary>
    /// <param name="ptr">The target raw pointer to copy the address of the current COM object to.</param>
    /// <returns>This method always returns <see cref="S_OK"/>.</returns>
    public readonly HRESULT CopyTo(T** ptr)
    {
        InternalAddRef();
        *ptr = (T*)_ptr;

        return HRESULT.S_OK;
    }

    /// <summary>Increments the reference count for the current COM object, if any, and copies its address to a target <see cref="ComPtr{T}"/>.</summary>
    /// <param name="p">The target raw pointer to copy the address of the current COM object to.</param>
    /// <returns>This method always returns <see cref="S_OK"/>.</returns>
    public readonly HRESULT CopyTo(ComPtr<T>* p)
    {
        InternalAddRef();
        *p->ReleaseAndGetAddressOf() = (T*)_ptr;

        return HRESULT.S_OK;
    }

    /// <summary>Increments the reference count for the current COM object, if any, and copies its address to a target <see cref="ComPtr{T}"/>.</summary>
    /// <param name="other">The target reference to copy the address of the current COM object to.</param>
    /// <returns>This method always returns <see cref="S_OK"/>.</returns>
    public readonly HRESULT CopyTo(ref ComPtr<T> other)
    {
        InternalAddRef();
        other.Attach((T*)_ptr);

        return HRESULT.S_OK;
    }

    /// <summary>Converts the current COM object reference to a given interface type and assigns that to a target raw pointer.</summary>
    /// <param name="ptr">The target raw pointer to copy the address of the current COM object to.</param>
    /// <returns>The result of <see cref="IUnknown.QueryInterface"/> for the target type <typeparamref name="U"/>.</returns>
    public readonly HRESULT CopyTo<U>(U** ptr) where U : unmanaged, IComIID
    {
        return _ptr->QueryInterface(__uuidof<U>(), (void**)ptr);
    }

    /// <summary>Converts the current COM object reference to a given interface type and assigns that to a target <see cref="ComPtr{T}"/>.</summary>
    /// <param name="p">The target raw pointer to copy the address of the current COM object to.</param>
    /// <returns>The result of <see cref="IUnknown.QueryInterface"/> for the target type <typeparamref name="U"/>.</returns>
    public readonly HRESULT CopyTo<U>(ComPtr<U>* p) where U : unmanaged, IComIID
    {
        return _ptr->QueryInterface(__uuidof<U>(), (void**)p->ReleaseAndGetAddressOf());
    }

    /// <summary>Converts the current COM object reference to a given interface type and assigns that to a target <see cref="ComPtr{T}"/>.</summary>
    /// <param name="other">The target reference to copy the address of the current COM object to.</param>
    /// <returns>The result of <see cref="IUnknown.QueryInterface"/> for the target type <typeparamref name="U"/>.</returns>
    public readonly HRESULT CopyTo<U>(ref ComPtr<U> other) where U : unmanaged, IComIID
    {
        U* ptr;
        HRESULT result = _ptr->QueryInterface(__uuidof<U>(), (void**)&ptr);

        other.Attach(ptr);

        return result;
    }

    /// <summary>Converts the current object reference to a type indicated by the given IID and assigns that to a target address.</summary>
    /// <param name="riid">The IID indicating the interface type to convert the COM object reference to.</param>
    /// <param name="ptr">The target raw pointer to copy the address of the current COM object to.</param>
    /// <returns>The result of <see cref="IUnknown.QueryInterface"/> for the target IID.</returns>
    public readonly HRESULT CopyTo(Guid* riid, void** ptr)
    {
        return _ptr->QueryInterface(riid, ptr);
    }

    /// <summary>Converts the current object reference to a type indicated by the given IID and assigns that to a target <see cref="ComPtr{T}"/> value.</summary>
    /// <param name="riid">The IID indicating the interface type to convert the COM object reference to.</param>
    /// <param name="p">The target raw pointer to copy the address of the current COM object to.</param>
    /// <returns>The result of <see cref="IUnknown.QueryInterface"/> for the target IID.</returns>
    public readonly HRESULT CopyTo(Guid* riid, ComPtr<IUnknown>* p)
    {
        return _ptr->QueryInterface(riid, (void**)p->ReleaseAndGetAddressOf());
    }

    /// <summary>Converts the current object reference to a type indicated by the given IID and assigns that to a target <see cref="ComPtr{T}"/> value.</summary>
    /// <param name="riid">The IID indicating the interface type to convert the COM object reference to.</param>
    /// <param name="other">The target reference to copy the address of the current COM object to.</param>
    /// <returns>The result of <see cref="IUnknown.QueryInterface"/> for the target IID.</returns>
    public readonly HRESULT CopyTo(Guid* riid, ref ComPtr<IUnknown> other)
    {
        IUnknown* ptr;
        HRESULT result = _ptr->QueryInterface(riid, (void**)&ptr);

        other.Attach(ptr);

        return result;
    }

    /// <inheritdoc/>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public void Dispose()
    {
        IUnknown* pointer = _ptr;

        if (pointer != null)
        {
            _ptr = null;

            _ = pointer->Release();
        }
    }

    /// <summary>Gets the currently wrapped raw pointer to a COM object.</summary>
    /// <returns>The raw pointer wrapped by the current <see cref="ComPtr{T}"/> instance.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public readonly T* Get()
    {
        return (T*)_ptr;
    }

    /// <summary>Gets the address of the current <see cref="ComPtr{T}"/> instance as a raw <typeparamref name="T"/> double pointer. This method is only valid when the current <see cref="ComPtr{T}"/> instance is on the stack or pinned.
    /// </summary>
    /// <returns>The raw pointer to the current <see cref="ComPtr{T}"/> instance.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public readonly T** GetAddressOf()
    {
        return (T**)Unsafe.AsPointer(ref Unsafe.AsRef(in this));
    }

    /// <summary>Releases the current COM object in use and gets the address of the <see cref="ComPtr{T}"/> instance as a raw <typeparamref name="T"/> double pointer. This method is only valid when the current <see cref="ComPtr{T}"/> instance is on the stack or pinned.</summary>
    /// <returns>The raw pointer to the current <see cref="ComPtr{T}"/> instance.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public T** ReleaseAndGetAddressOf()
    {
        _ = InternalRelease();

        return GetAddressOf();
    }

    /// <summary>Resets the current instance by decrementing the reference count for the target COM object and setting the internal raw pointer to <see langword="null"/>.</summary>
    /// <returns>The updated reference count for the COM object that was in use, if any.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public uint Reset()
    {
        return InternalRelease();
    }

    /// <summary>Swaps the current COM object reference with that of a given <see cref="ComPtr{T}"/> instance.</summary>
    /// <param name="r">The target <see cref="ComPtr{T}"/> instance to swap with the current one.</param>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public void Swap(ComPtr<T>* r)
    {
        IUnknown* tmp = _ptr;
        _ptr = r->_ptr;
        r->_ptr = tmp;
    }

    /// <summary>Swaps the current COM object reference with that of a given <see cref="ComPtr{T}"/> instance.</summary>
    /// <param name="other">The target <see cref="ComPtr{T}"/> instance to swap with the current one.</param>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public void Swap(ref ComPtr<T> other)
    {
        IUnknown* tmp = _ptr;
        _ptr = other._ptr;
        other._ptr = tmp;
    }

    // Increments the reference count for the current COM object, if any
    private readonly void InternalAddRef()
    {
        IUnknown* temp = _ptr;

        if (temp != null)
        {
            _ = temp->AddRef();
        }
    }

    // Decrements the reference count for the current COM object, if any
    private uint InternalRelease()
    {
        uint @ref = 0;
        IUnknown* temp = _ptr;

        if (temp != null)
        {
            _ptr = null;
            @ref = temp->Release();
        }

        return @ref;
    }
}
