﻿/*
---------------------------------------------------------------------------------------------------------

                      `@@@.  @@@@. :@@@@@   #@@@;  @    @  '@@@+         `@@+          
                      @# +@ @@  '@`;@#  @@ #@` ,@; @    @ '@, .@#     `@ @@@@@.        
                      @@  ;`@    @@;@   '@ @`   ;@ @    @ @,   .@     @@ @@@@@@`       
                      `@@@ :@@@@@@@;@   .@ @@@@@@@ @    @ @@@@@@@    @@@ @@@@@@@       
                      + `@@,@      ;@   .@ @       @    @ @          @@@  @@@@@@       
                      @  `@ @'   @;;@   .@ @@   @@ @#  :@ @@   @@    @@@@  #;`@@       
                      @@@@@ .@@@@@ ;@   .@  @@@@@  ;@@@@#  @@@@@     @@@@`   @@@       
                                                     @@`             @@@@@`  @@@       
                                                     @@              `@@@@   #@,       
                                                                      +@@#@@@@#        
                                                     @@                ,@@@@@:         
              																		 
----------------------------------------------------------------------------------------------------------

 THE SAMPLE CODE IS PROVIDED “AS IS” AND ANY EXPRESS OR IMPLIED WARRANTIES,
 INCLUDING THE IMPLIED WARRANTIES OF MERCHANTABILITY AND FITNESS FOR A PARTICULAR PURPOSE ARE DISCLAIMED.
 IN NO EVENT SHALL PAGERDUTY OR CONTRIBUTORS BE LIABLE FOR ANY DIRECT, INDIRECT, INCIDENTAL, SPECIAL, 
 EXEMPLARY, OR CONSEQUENTIAL DAMAGES (INCLUDING, BUT NOT LIMITED TO, PROCUREMENT OF SUBSTITUTE GOODS 
 OR SERVICES; LOSS OF USE, DATA, OR PROFITS; OR BUSINESS INTERRUPTION) SUSTAINED BY YOU OR A THIRD PARTY,
 HOWEVER CAUSED AND ON ANY THEORY OF LIABILITY, WHETHER IN CONTRACT, STRICT LIABILITY, OR TORT ARISING 
 IN ANY WAY OUT OF THE USE OF THIS SAMPLE CODE, EVEN IF ADVISED OF THE POSSIBILITY OF SUCH DAMAGE.
 
 The Code is not covered by any Seneye Service Level Agreements.

----------------------------------------------------------------------------------------------------------
*/ 
namespace Seneye.SUD.ListeningDriver 
{
    using System;
    using System.Linq;
    using System.Reactive;
    using System.Reactive.Linq;
    static class ReactiveExtensions
    {
        public static IObservable<TRet> ContinueAfter<T, TRet>(this IObservable<T> observable, Func<IObservable<TRet>> selector)
        {
            return observable.AsCompletion<T>().SelectMany((Unit _) => selector());
        }
        public static IObservable<TRet> ContinueAfter<T, TRet>(this IObservable<T> observable, Func<IObservable<TRet>> selector, System.Reactive.Concurrency.IScheduler scheduler)
        {
            return observable.AsCompletion<T>().ObserveOn(scheduler).SelectMany((Unit _) => selector());
        }

        public static IObservable<Unit> AsCompletion<T>(this IObservable<T> observable)
        {
            return observable.Aggregate(Unit.Default, (Unit unit, T _) => unit);
        }

    }
}
