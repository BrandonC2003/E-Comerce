﻿//------------------------------------------------------------------------------
// <auto-generated>
//     Este código fue generado por una herramienta.
//     Versión de runtime:4.0.30319.42000
//
//     Los cambios en este archivo podrían causar un comportamiento incorrecto y se perderán si
//     se vuelve a generar el código.
// </auto-generated>
//------------------------------------------------------------------------------

namespace E_Comerce.ServiceReference1 {
    using System.Runtime.Serialization;
    using System;
    using System.ComponentModel.DataAnnotations;
    
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Runtime.Serialization", "4.0.0.0")]
    [System.Runtime.Serialization.DataContractAttribute(Name="CompositeType", Namespace="http://schemas.datacontract.org/2004/07/WCFLogin")]
    [System.SerializableAttribute()]
    public partial class CompositeType : object, System.Runtime.Serialization.IExtensibleDataObject, System.ComponentModel.INotifyPropertyChanged {
        
        [System.NonSerializedAttribute()]
        private System.Runtime.Serialization.ExtensionDataObject extensionDataField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private bool BoolValueField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string StringValueField;
        
        [global::System.ComponentModel.BrowsableAttribute(false)]
        public System.Runtime.Serialization.ExtensionDataObject ExtensionData {
            get {
                return this.extensionDataField;
            }
            set {
                this.extensionDataField = value;
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public bool BoolValue {
            get {
                return this.BoolValueField;
            }
            set {
                if ((this.BoolValueField.Equals(value) != true)) {
                    this.BoolValueField = value;
                    this.RaisePropertyChanged("BoolValue");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string StringValue {
            get {
                return this.StringValueField;
            }
            set {
                if ((object.ReferenceEquals(this.StringValueField, value) != true)) {
                    this.StringValueField = value;
                    this.RaisePropertyChanged("StringValue");
                }
            }
        }
        
        public event System.ComponentModel.PropertyChangedEventHandler PropertyChanged;
        
        protected void RaisePropertyChanged(string propertyName) {
            System.ComponentModel.PropertyChangedEventHandler propertyChanged = this.PropertyChanged;
            if ((propertyChanged != null)) {
                propertyChanged(this, new System.ComponentModel.PropertyChangedEventArgs(propertyName));
            }
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Runtime.Serialization", "4.0.0.0")]
    [System.Runtime.Serialization.DataContractAttribute(Name="RespuestaUsuario", Namespace="http://schemas.datacontract.org/2004/07/WCFLogin")]
    [System.SerializableAttribute()]
    public partial class RespuestaUsuario : object, System.Runtime.Serialization.IExtensibleDataObject, System.ComponentModel.INotifyPropertyChanged {
        
        [System.NonSerializedAttribute()]
        private System.Runtime.Serialization.ExtensionDataObject extensionDataField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string ContrasenaField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private int ID_RolField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private int ID_UsuarioField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string NombreUsuarioField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string UsuarioField;
        
        [global::System.ComponentModel.BrowsableAttribute(false)]
        public System.Runtime.Serialization.ExtensionDataObject ExtensionData {
            get {
                return this.extensionDataField;
            }
            set {
                this.extensionDataField = value;
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        [DataType(DataType.Password)]
        public string Contrasena {
            get {
                return this.ContrasenaField;
            }
            set {
                if ((object.ReferenceEquals(this.ContrasenaField, value) != true)) {
                    this.ContrasenaField = value;
                    this.RaisePropertyChanged("Contrasena");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public int ID_Rol {
            get {
                return this.ID_RolField;
            }
            set {
                if ((this.ID_RolField.Equals(value) != true)) {
                    this.ID_RolField = value;
                    this.RaisePropertyChanged("ID_Rol");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public int ID_Usuario {
            get {
                return this.ID_UsuarioField;
            }
            set {
                if ((this.ID_UsuarioField.Equals(value) != true)) {
                    this.ID_UsuarioField = value;
                    this.RaisePropertyChanged("ID_Usuario");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string NombreUsuario {
            get {
                return this.NombreUsuarioField;
            }
            set {
                if ((object.ReferenceEquals(this.NombreUsuarioField, value) != true)) {
                    this.NombreUsuarioField = value;
                    this.RaisePropertyChanged("NombreUsuario");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string Usuario {
            get {
                return this.UsuarioField;
            }
            set {
                if ((object.ReferenceEquals(this.UsuarioField, value) != true)) {
                    this.UsuarioField = value;
                    this.RaisePropertyChanged("Usuario");
                }
            }
        }
        
        public event System.ComponentModel.PropertyChangedEventHandler PropertyChanged;
        
        protected void RaisePropertyChanged(string propertyName) {
            System.ComponentModel.PropertyChangedEventHandler propertyChanged = this.PropertyChanged;
            if ((propertyChanged != null)) {
                propertyChanged(this, new System.ComponentModel.PropertyChangedEventArgs(propertyName));
            }
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Runtime.Serialization", "4.0.0.0")]
    [System.Runtime.Serialization.DataContractAttribute(Name="CambiarClave", Namespace="http://schemas.datacontract.org/2004/07/WCFLogin")]
    [System.SerializableAttribute()]
    public partial class CambiarClave : object, System.Runtime.Serialization.IExtensibleDataObject, System.ComponentModel.INotifyPropertyChanged {
        
        [System.NonSerializedAttribute()]
        private System.Runtime.Serialization.ExtensionDataObject extensionDataField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string ClaveActField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string ClaveNewField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private int ID_UsuarioField;
        
        [global::System.ComponentModel.BrowsableAttribute(false)]
        public System.Runtime.Serialization.ExtensionDataObject ExtensionData {
            get {
                return this.extensionDataField;
            }
            set {
                this.extensionDataField = value;
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string ClaveAct {
            get {
                return this.ClaveActField;
            }
            set {
                if ((object.ReferenceEquals(this.ClaveActField, value) != true)) {
                    this.ClaveActField = value;
                    this.RaisePropertyChanged("ClaveAct");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string ClaveNew {
            get {
                return this.ClaveNewField;
            }
            set {
                if ((object.ReferenceEquals(this.ClaveNewField, value) != true)) {
                    this.ClaveNewField = value;
                    this.RaisePropertyChanged("ClaveNew");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public int ID_Usuario {
            get {
                return this.ID_UsuarioField;
            }
            set {
                if ((this.ID_UsuarioField.Equals(value) != true)) {
                    this.ID_UsuarioField = value;
                    this.RaisePropertyChanged("ID_Usuario");
                }
            }
        }
        
        public event System.ComponentModel.PropertyChangedEventHandler PropertyChanged;
        
        protected void RaisePropertyChanged(string propertyName) {
            System.ComponentModel.PropertyChangedEventHandler propertyChanged = this.PropertyChanged;
            if ((propertyChanged != null)) {
                propertyChanged(this, new System.ComponentModel.PropertyChangedEventArgs(propertyName));
            }
        }
    }
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ServiceModel.ServiceContractAttribute(ConfigurationName="ServiceReference1.IService1")]
    public interface IService1 {
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IService1/GetData", ReplyAction="http://tempuri.org/IService1/GetDataResponse")]
        string GetData(int value);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IService1/GetData", ReplyAction="http://tempuri.org/IService1/GetDataResponse")]
        System.Threading.Tasks.Task<string> GetDataAsync(int value);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IService1/GetDataUsingDataContract", ReplyAction="http://tempuri.org/IService1/GetDataUsingDataContractResponse")]
        E_Comerce.ServiceReference1.CompositeType GetDataUsingDataContract(E_Comerce.ServiceReference1.CompositeType composite);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IService1/GetDataUsingDataContract", ReplyAction="http://tempuri.org/IService1/GetDataUsingDataContractResponse")]
        System.Threading.Tasks.Task<E_Comerce.ServiceReference1.CompositeType> GetDataUsingDataContractAsync(E_Comerce.ServiceReference1.CompositeType composite);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IService1/InicioSesion", ReplyAction="http://tempuri.org/IService1/InicioSesionResponse")]
        E_Comerce.ServiceReference1.RespuestaUsuario InicioSesion(E_Comerce.ServiceReference1.RespuestaUsuario datos);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IService1/InicioSesion", ReplyAction="http://tempuri.org/IService1/InicioSesionResponse")]
        System.Threading.Tasks.Task<E_Comerce.ServiceReference1.RespuestaUsuario> InicioSesionAsync(E_Comerce.ServiceReference1.RespuestaUsuario datos);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IService1/CambiarClave", ReplyAction="http://tempuri.org/IService1/CambiarClaveResponse")]
        E_Comerce.ServiceReference1.CambiarClaveResponse CambiarClave(E_Comerce.ServiceReference1.CambiarClaveRequest request);
        
        // CODEGEN: Generando contrato de mensaje, ya que la operación tiene múltiples valores de devolución.
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IService1/CambiarClave", ReplyAction="http://tempuri.org/IService1/CambiarClaveResponse")]
        System.Threading.Tasks.Task<E_Comerce.ServiceReference1.CambiarClaveResponse> CambiarClaveAsync(E_Comerce.ServiceReference1.CambiarClaveRequest request);
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ServiceModel.MessageContractAttribute(WrapperName="CambiarClave", WrapperNamespace="http://tempuri.org/", IsWrapped=true)]
    public partial class CambiarClaveRequest {
        
        [System.ServiceModel.MessageBodyMemberAttribute(Namespace="http://tempuri.org/", Order=0)]
        public E_Comerce.ServiceReference1.CambiarClave resp;
        
        public CambiarClaveRequest() {
        }
        
        public CambiarClaveRequest(E_Comerce.ServiceReference1.CambiarClave resp) {
            this.resp = resp;
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ServiceModel.MessageContractAttribute(WrapperName="CambiarClaveResponse", WrapperNamespace="http://tempuri.org/", IsWrapped=true)]
    public partial class CambiarClaveResponse {
        
        [System.ServiceModel.MessageBodyMemberAttribute(Namespace="http://tempuri.org/", Order=0)]
        public bool CambiarClaveResult;
        
        [System.ServiceModel.MessageBodyMemberAttribute(Namespace="http://tempuri.org/", Order=1)]
        public string mensaje;
        
        public CambiarClaveResponse() {
        }
        
        public CambiarClaveResponse(bool CambiarClaveResult, string mensaje) {
            this.CambiarClaveResult = CambiarClaveResult;
            this.mensaje = mensaje;
        }
    }
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public interface IService1Channel : E_Comerce.ServiceReference1.IService1, System.ServiceModel.IClientChannel {
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public partial class Service1Client : System.ServiceModel.ClientBase<E_Comerce.ServiceReference1.IService1>, E_Comerce.ServiceReference1.IService1 {
        
        public Service1Client() {
        }
        
        public Service1Client(string endpointConfigurationName) : 
                base(endpointConfigurationName) {
        }
        
        public Service1Client(string endpointConfigurationName, string remoteAddress) : 
                base(endpointConfigurationName, remoteAddress) {
        }
        
        public Service1Client(string endpointConfigurationName, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(endpointConfigurationName, remoteAddress) {
        }
        
        public Service1Client(System.ServiceModel.Channels.Binding binding, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(binding, remoteAddress) {
        }
        
        public string GetData(int value) {
            return base.Channel.GetData(value);
        }
        
        public System.Threading.Tasks.Task<string> GetDataAsync(int value) {
            return base.Channel.GetDataAsync(value);
        }
        
        public E_Comerce.ServiceReference1.CompositeType GetDataUsingDataContract(E_Comerce.ServiceReference1.CompositeType composite) {
            return base.Channel.GetDataUsingDataContract(composite);
        }
        
        public System.Threading.Tasks.Task<E_Comerce.ServiceReference1.CompositeType> GetDataUsingDataContractAsync(E_Comerce.ServiceReference1.CompositeType composite) {
            return base.Channel.GetDataUsingDataContractAsync(composite);
        }
        
        public E_Comerce.ServiceReference1.RespuestaUsuario InicioSesion(E_Comerce.ServiceReference1.RespuestaUsuario datos) {
            return base.Channel.InicioSesion(datos);
        }
        
        public System.Threading.Tasks.Task<E_Comerce.ServiceReference1.RespuestaUsuario> InicioSesionAsync(E_Comerce.ServiceReference1.RespuestaUsuario datos) {
            return base.Channel.InicioSesionAsync(datos);
        }
        
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
        E_Comerce.ServiceReference1.CambiarClaveResponse E_Comerce.ServiceReference1.IService1.CambiarClave(E_Comerce.ServiceReference1.CambiarClaveRequest request) {
            return base.Channel.CambiarClave(request);
        }
        
        public bool CambiarClave(E_Comerce.ServiceReference1.CambiarClave resp, out string mensaje) {
            E_Comerce.ServiceReference1.CambiarClaveRequest inValue = new E_Comerce.ServiceReference1.CambiarClaveRequest();
            inValue.resp = resp;
            E_Comerce.ServiceReference1.CambiarClaveResponse retVal = ((E_Comerce.ServiceReference1.IService1)(this)).CambiarClave(inValue);
            mensaje = retVal.mensaje;
            return retVal.CambiarClaveResult;
        }
        
        public System.Threading.Tasks.Task<E_Comerce.ServiceReference1.CambiarClaveResponse> CambiarClaveAsync(E_Comerce.ServiceReference1.CambiarClaveRequest request) {
            return base.Channel.CambiarClaveAsync(request);
        }
    }
}
