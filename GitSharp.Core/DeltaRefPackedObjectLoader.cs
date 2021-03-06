﻿/*
 * Copyright (C) 2007, Robin Rosenberg <robin.rosenberg@dewire.com>
 * Copyright (C) 2008, Shawn O. Pearce <spearce@spearce.org>
 * Copyright (C) 2009, Henon <meinrad.recheis@gmail.com>
 *
 * All rights reserved.
 *
 * Redistribution and use in source and binary forms, with or
 * without modification, are permitted provided that the following
 * conditions are met:
 *
 * - Redistributions of source code must retain the above copyright
 *   notice, this list of conditions and the following disclaimer.
 *
 * - Redistributions in binary form must reproduce the above
 *   copyright notice, this list of conditions and the following
 *   disclaimer in the documentation and/or other materials provided
 *   with the distribution.
 *
 * - Neither the name of the Git Development Community nor the
 *   names of its contributors may be used to endorse or promote
 *   products derived from this software without specific prior
 *   written permission.
 *
 * THIS SOFTWARE IS PROVIDED BY THE COPYRIGHT HOLDERS AND
 * CONTRIBUTORS "AS IS" AND ANY EXPRESS OR IMPLIED WARRANTIES,
 * INCLUDING, BUT NOT LIMITED TO, THE IMPLIED WARRANTIES
 * OF MERCHANTABILITY AND FITNESS FOR A PARTICULAR PURPOSE
 * ARE DISCLAIMED. IN NO EVENT SHALL THE COPYRIGHT OWNER OR
 * CONTRIBUTORS BE LIABLE FOR ANY DIRECT, INDIRECT, INCIDENTAL,
 * SPECIAL, EXEMPLARY, OR CONSEQUENTIAL DAMAGES (INCLUDING, BUT
 * NOT LIMITED TO, PROCUREMENT OF SUBSTITUTE GOODS OR SERVICES;
 * LOSS OF USE, DATA, OR PROFITS; OR BUSINESS INTERRUPTION) HOWEVER
 * CAUSED AND ON ANY THEORY OF LIABILITY, WHETHER IN CONTRACT,
 * STRICT LIABILITY, OR TORT (INCLUDING NEGLIGENCE OR OTHERWISE)
 * ARISING IN ANY WAY OUT OF THE USE OF THIS SOFTWARE, EVEN IF
 * ADVISED OF THE POSSIBILITY OF SUCH DAMAGE.
 */

using GitSharp.Core.Exceptions;

namespace GitSharp.Core
{
    /// <summary>
	/// Reads a deltified object which uses an <see cref="ObjectId"/> to find its base.
    /// </summary>
    public class DeltaRefPackedObjectLoader : DeltaPackedObjectLoader
    {
        private readonly ObjectId _deltaBase;

        public DeltaRefPackedObjectLoader(PackFile pr, long dataOffset, long objectOffset, int deltaSz, ObjectId @base)
            : base(pr, dataOffset, objectOffset, deltaSz)
        {
            _deltaBase = @base;
        }

        public override PackedObjectLoader GetBaseLoader(WindowCursor curs)
        {
            PackedObjectLoader or = PackFile.Get(curs, _deltaBase);
            if (or == null)
            {
            	throw new MissingObjectException(_deltaBase, "delta base");
            }
            return or;
        }

    	public override int RawType
    	{
    		get { return Constants.OBJ_REF_DELTA; }
    	}

    	public override ObjectId DeltaBase
    	{
    		get { return _deltaBase; }
    	}
    }
}