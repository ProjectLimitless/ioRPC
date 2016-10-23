/** 
 * This file is part of ioRPC.
 * Copyright © 2016 Donovan Solms.
 * Project Limitless
 * https://www.projectlimitless.io
 * 
 * ioRPC and Project Limitless is free software: you can redistribute it and/or modify
 * it under the terms of the Apache License Version 2.0.
 * 
 * You should have received a copy of the Apache License Version 2.0 with
 * ioRPC. If not, see http://www.apache.org/licenses/LICENSE-2.0.
 */

using System;

namespace Limitless.ioRPC.Interfaces
{
    /// <summary>
    /// Adds async result capabilities to a handler.
    /// </summary>
    public interface IRPCAsyncHandler
    {
        /// <summary>
        /// Sets the callback for async results.
        /// </summary>
        /// <param name="asyncCallback">The callback function</param>
        void SetAsyncCallback(Action<string, object> asyncCallback);
    }
}
