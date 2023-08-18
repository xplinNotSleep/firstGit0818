using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;
using System.Windows.Forms;

namespace System
{
    /// <summary>
    /// 与Type类型完全对应，用于通过库名和类名构造Type
    /// </summary>
    public class TypeInfo
    {
        public string AssemblyName { get; set; }

        public string TypeName { get; set; }

        public TypeInfo(string _assemblyName, string _typeName) { AssemblyName = _assemblyName; TypeName = _typeName; }

        public TypeInfo(Type type) : this(type.Assembly.FullName, type.Name) { }

        public override int GetHashCode() { return ((Type)this).GetHashCode(); }

        public override bool Equals(object o) { return GetHashCode() == o.GetHashCode(); }

        public static implicit operator TypeInfo(Type type) { return new TypeInfo(type); }

        public static implicit operator Type(TypeInfo typeInfo) { return Assembly.Load(typeInfo.AssemblyName).GetType(typeInfo.TypeName); }

        public static bool operator ==(Type type, TypeInfo typeInfo) { return type == (Type)typeInfo; }

        public static bool operator !=(Type type, TypeInfo typeInfo) { return type != (Type)typeInfo; }

        public static bool operator ==(TypeInfo typeInfo, Type type) { return type == (Type)typeInfo; }

        public static bool operator !=(TypeInfo typeInfo, Type type) { return type != (Type)typeInfo; }

        public static bool operator ==(TypeInfo typeInfo1, TypeInfo typeInfo2) { return typeInfo1.TypeName == typeInfo2.TypeName && typeInfo1.AssemblyName == typeInfo2.AssemblyName; }

        public static bool operator !=(TypeInfo typeInfo1, TypeInfo typeInfo2) { return typeInfo1.TypeName != typeInfo2.TypeName && typeInfo1.AssemblyName == typeInfo2.AssemblyName; }

        public T Instance<T>(Type[] createParamsTypes, object[] createParams)
        {
            return (T)((Type)this).GetConstructor(createParamsTypes == null ? new Type[] { } : createParamsTypes).Invoke(createParams == null ? new object[] { } : createParams);
        }

        public ReflectStatic ReflectStatic()
        {
            return new ReflectStatic(this);
        }

        public ReflectInstance ReflectInstance(Type[] createParamsTypes, object[] createParams)
        {
            return new ReflectInstance(this, createParamsTypes, createParams);
        }
    }
    /// <summary>
    /// 扩展Type方法
    /// </summary>
    public static class TypeExtension
    {
        public static T Instance<T>(this Type type, Type[] createParamsTypes, object[] createParams)
        {
            return (T)type.GetConstructor(createParamsTypes == null ? new Type[] { } : createParamsTypes).Invoke(createParams == null ? new object[] { } : createParams);
        }

        public static ReflectStatic ReflectStatic(this Type type)
        {
            return new ReflectStatic(type);
        }

        public static ReflectInstance ReflectInstance(this Type type, Type[] createParamsTypes, object[] createParams)
        {
            return new ReflectInstance(type, createParamsTypes, createParams);
        }    
    }
    /// <summary>
    /// 反射属性类
    /// </summary>
    public class ReflectProperty
    {
        protected ReflectObject m_object;

        public virtual object this[string property]
        {
            get
            {
                PropertyInfo propertyInfo = m_object.Type.GetProperties(m_object.BindFlags).FirstOrDefault(p => p.Name == property);
                if (propertyInfo == null)
                    throw new ArgumentException("Property \'" + property + "\' not found");
                return propertyInfo.GetValue(m_object.Instance, null);
            }
            set
            {
                PropertyInfo propertyInfo = m_object.Type.GetProperties(m_object.BindFlags).FirstOrDefault(p => p.Name == property);
                if (propertyInfo == null)
                    throw new ArgumentException("Property \'" + property + "\' not found");
                propertyInfo.SetValue(m_object.Instance, value, null);
            }
        }

        public ReflectProperty(ReflectObject reflectObject)
        {
            m_object = reflectObject;
        }
    }
    /// <summary>
    /// 反射属性类,返回ReflectInstance
    /// </summary>
    public class ReflectPropertyR
    {
        protected ReflectObject m_object;

        public virtual ReflectInstance this[string property]
        {
            get
            {
                object result = m_object.Property[property];
                if (result == null)
                    return null;
                return new ReflectInstance(result);
            }
            set { m_object.Property[property] = value.Instance; }
        }

        public ReflectPropertyR(ReflectObject reflectObject)
        {
            m_object = reflectObject;
        }
    }
    /// <summary>
    /// 反射索引属性类
    /// </summary>
    public class ReflectItem
    {
        protected ReflectObject m_object;

        public virtual object this[params object[] idxs]
        {
            get
            {
                PropertyInfo propertyInfo = m_object.Type.GetProperties(m_object.BindFlags).FirstOrDefault(p => p.Name == "Item");
                if (propertyInfo == null)
                    throw new ArgumentException("Property \'Item\' not found");
                return propertyInfo.GetValue(m_object.Instance, idxs);
            }
            set
            {
                PropertyInfo propertyInfo = m_object.Type.GetProperties(m_object.BindFlags).FirstOrDefault(p => p.Name == "Item");
                if (propertyInfo == null)
                    throw new ArgumentException("Property \'Item\' not found");
                propertyInfo.SetValue(m_object.Instance, value, idxs); 
            }
        }

        public ReflectItem(ReflectObject reflectObject)
        {
            m_object = reflectObject;
        }
    }
    /// <summary>
    /// 反射索引属性类,返回ReflectInstance
    /// </summary>
    public class ReflectItemR
    {
        protected ReflectObject m_object;

        public virtual ReflectInstance this[params object[] idxs]
        {
            get
            {
                object result = m_object.Item[idxs];
                if (result == null)
                    return null;
                return new ReflectInstance(result);
            }
            set { m_object.Item[idxs] = value.Instance; }
        }

        public ReflectItemR(ReflectObject reflectObject)
        {
            m_object = reflectObject;
        }
    }
    /// <summary>
    /// 反射封装类，对于静态请使用派生类ReflectStatic，对于实例请使用派生类ReflectInstance
    /// </summary>
    public class ReflectObject
    {
        protected BindingFlags m_bindFlags = BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance;
        public virtual BindingFlags BindFlags { get { return m_bindFlags; } set { m_bindFlags = value; } }

        protected Type m_type;
        public virtual Type Type { get { return m_type; } set { m_type = value; } }
        public virtual TypeInfo TypeInfo { get { return m_type; } set { m_type = value; } }

        protected object m_instance;
        public virtual object Instance { get { return m_instance; } set { m_instance = value; } }

        protected ReflectProperty m_property;
        public virtual ReflectProperty Property { get { return m_property; } set { m_property = value; } }

        protected ReflectPropertyR m_propertyR;
        public virtual ReflectPropertyR PropertyR { get { return m_propertyR; } set { m_propertyR = value; } }

        protected ReflectItem m_item;
        public virtual ReflectItem Item { get { return m_item; } set { m_item = value; } }

        protected ReflectItemR m_itemR;
        public virtual ReflectItemR ItemR { get { return m_itemR; } set { m_itemR = value; } }

        public virtual object Invoke(string method, params object[] parameters)
        {
            MethodInfo methodInfo = m_type.GetMethods(m_bindFlags).FirstOrDefault(p => p.Name == method);
            if (methodInfo == null)
                throw new ArgumentException("Method \'" + method + "\' not found");
            return methodInfo.Invoke(m_instance, parameters);
        }

        public virtual ReflectInstance InvokeR(string method, params object[] parameters)
        {
            object result = Invoke(method, parameters);
            if (result == null)
                return null;
            return new ReflectInstance(result);
        }

        public virtual T Invoke<T>(string method, params object[] parameters)
        {
            return (T)Invoke(method, parameters);
        }

        protected ReflectObject(Type type, object instance, BindingFlags bindFlags)
        {
            m_type = type;
            Instance = instance;
            m_bindFlags = bindFlags;
            m_property = new ReflectProperty(this);
            m_item = new ReflectItem(this);
        }
    }
    /// <summary>
    /// 反射封装类，用于静态方法和静态属性(/索引属性)
    /// </summary>
    public class ReflectStatic : ReflectObject
    {
        public ReflectStatic(Type type)
            : base(type, null, BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Static)
        {
        }

        public ReflectStatic(TypeInfo type)
            : base((Type)type, null, BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Static)
        {
        }

        public ReflectStatic(ReflectObject instance)
            : base(instance.Type, null, BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Static)
        {
        }

        public ReflectInstance ReflectInstance(Type[] createParamsTypes, object[] createParams)
        {
            return new ReflectInstance(Type, createParamsTypes, createParams);
        }
    }
    /// <summary>
    /// 反射封装类，用于实例方法和实例属性(/索引属性)
    /// </summary>
    public class ReflectInstance : ReflectObject
    {
        public ReflectInstance(object instance)
            : base(instance.GetType(), instance, BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance)
        {
        }

        public ReflectInstance(Type type, Type[] createParamsTypes, object[] createParams)
            : base((Type)type,
            type.GetConstructor(createParamsTypes == null ? new Type[] { } : createParamsTypes).Invoke(createParams == null ? new object[] { } : createParams),
            BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance)
        {
        }

        public ReflectInstance(ReflectObject instance)
            : base(instance.Type, instance.Instance, BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance)
        {
        }

        public ReflectStatic ReflectStatic()
        {
            return new ReflectStatic(Type);
        }
    }
}
