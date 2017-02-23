if (process.env.enableReactTesting) {
    global.ReactTesting = {
        addRenderContainer: () => {},
        removeRenderContainer: () => {},
    };

    function stringifySafe(value) {
        if (typeof value === 'string') {
            return value;
        }
        if (value === undefined || value === null) {
            return '';
        }
        try {
            return JSON.stringify(value);
        }
        catch (e) {
            return '';
        }
    }

    function fixInstance(instance) {
        var instanceProps = instance._currentElement.props;
        if (instance._currentElement._owner && instance._currentElement._owner._currentElement.type) {
            fixInstance(instance._currentElement._owner);
        }    
        if (instance._renderedComponent && instanceProps && (instanceProps['data-tid'] || instance._currentElement.type.name)) {
            var result = instance._renderedComponent._hostNode || (instance._renderedComponent._renderedComponent && instance._renderedComponent._renderedComponent._hostNode);
            if (result && result.attributes && instanceProps) {
                if (instanceProps['data-tid']) {
                    var dataTidAttr = document.createAttribute("data-tid");
                    dataTidAttr.value = instanceProps['data-tid'];
                    result.attributes && (result.attributes.setNamedItem(dataTidAttr));
                }
                for (var prop in instanceProps) {
                    if (!prop.startsWith('$$') && !prop.startsWith('on') && (prop !== 'children')) {
                        if (typeof instanceProps[prop] !== 'function') {
                            var attr = document.createAttribute(`data-prop-${prop}`);
                            attr.value = stringifySafe(instanceProps[prop]);
                            result.attributes && (result.attributes.setNamedItem(attr));
                        }
                    }
                }
            }                  
        }
    }

    if (!global.__REACT_DEVTOOLS_GLOBAL_HOOK__) {
        global.__REACT_DEVTOOLS_GLOBAL_HOOK__ = {
            inject: () => {},
        };
    }    
    var oldInject = global.__REACT_DEVTOOLS_GLOBAL_HOOK__.inject;
    global.__REACT_DEVTOOLS_GLOBAL_HOOK__.inject = x => {
        if (oldInject) {
            oldInject(x);
        }            
        var oldreceiveComponent = x.Reconciler.receiveComponent;
        x.Reconciler.receiveComponent = function(internalInstance, nextElement, transaction, context) {
            oldreceiveComponent(internalInstance, nextElement, transaction, context);

            var prevElement = internalInstance._currentElement;
            if (nextElement === prevElement && context === internalInstance._context) {
                return;
            }

            if (internalInstance._currentElement && internalInstance._currentElement.type) {
                var instance = internalInstance;
                fixInstance(instance);
            }
        }


        var oldMountComponent = x.Reconciler.mountComponent.bind(x.Reconciler);
        x.Reconciler.mountComponent = (instance, tr, host, hostParent, hostContainerInfo, context, ...rest) => {        
            if (instance._currentElement && instance._currentElement.props && (instance._currentElement.props['data-tid'] || instance._currentElement.type.name)) {
                
                var result = oldMountComponent(instance, tr, host,  hostParent, hostContainerInfo, context, ...rest);

                if (result.node && result.node.attributes) {
                    if (instance._currentElement.props['data-tid']) {
                        var dataTidAttr = document.createAttribute("data-tid");
                        dataTidAttr.value = instance._currentElement.props['data-tid'];
                        result.node.attributes && (result.node.attributes.setNamedItem(dataTidAttr));
                    }
                    if (instance._currentElement.type.name) {
                        var dataComponentNameAttr = document.createAttribute("data-comp-name");
                        dataComponentNameAttr.value = instance._currentElement.type.name;
                        result.node.attributes && (result.node.attributes.setNamedItem(dataComponentNameAttr));
                    }                
                    if (instance._currentElement.props) {
                        for (var prop in instance._currentElement.props) {
                            if (!prop.startsWith('$$') && !prop.startsWith('on') && (prop !== 'children')) {
                                if (typeof instance._currentElement.props[prop] !== 'function') {
                                    var attr = document.createAttribute(`data-prop-${prop}`);
                                    attr.value = stringifySafe(instance._currentElement.props[prop]);
                                    result.node.attributes && (result.node.attributes.setNamedItem(attr));
                                }
                            }
                        }
                    }           
                    if (instance._currentElement._owner) {
                        var ownerInstance = instance._currentElement._owner;
                        if (ownerInstance._rootNodeID === instance._rootNodeID) { 
                            if (getDomHostNode(ownerInstance) === getDomHostNode(instance)) {
                                if (ownerInstance._currentElement.props['data-tid']) {
                                    var dataTidAttr = document.createAttribute("data-tid");
                                    dataTidAttr.value = ownerInstance._currentElement.props['data-tid'];
                                    result.node.attributes && (result.node.attributes.setNamedItem(dataTidAttr));
                                }
                            }
                        }

                    }
                    return result;
                }
                return result;
            }
            return oldMountComponent(instance, tr, host,  hostParent, hostContainerInfo, context, ...rest);
        }
    };

    function getDomHostNode(internalInstance) {
        if (internalInstance._hostNode) {
            return internalInstance._hostNode;
        }
        if (internalInstance._renderedComponent) {
            return getDomHostNode(internalInstance._renderedComponent);
        }
        return null;
    }
}
