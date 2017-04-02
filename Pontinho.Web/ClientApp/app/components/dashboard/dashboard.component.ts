import { Component, OnInit } from '@angular/core';
var $;

@Component({
    templateUrl: 'dashboard.component.html'
})

export class DashboardComponent implements OnInit {
    charts = [];
    model = {};

    constructor() { }

    ngOnInit() {
        // var _this = this;
        // this.competitionService.getDashboard().success(function (result) {
        //     _this.model = result;
        //     _this.initCharts();
        // });
    }

    // initCharts() {
    //     var perMWin = this.model.matchesWon * 100 / this.model.totalMatches;
    //     this.initChart("#ct-chart-m", [perMWin, 100 - perMWin]);
    //     var perRWin = this.model.roundsWon * 100 / this.model.totalRounds;
    //     this.initChart("#ct-chart-r", [perRWin, 100 - perRWin]);
    //     var perRSnapped = this.model.roundsSnapped * 100 / this.model.totalRounds;
    //     this.initChart("#ct-chart-rs", [perRSnapped, 100 - perRSnapped]);
    // }

    // initChart(obj, series) {
    //     if ($(obj).length) {
    //         this.charts.pieChartRS = Chartist.Pie(obj, {
    //             series: series
    //         }, {
    //                 donut: true,
    //                 donutWidth: 30,
    //                 startAngle: 270,
    //                 total: 200,
    //                 showLabel: false,
    //                 chartPadding: 0,
    //             });
    //     }
    // }
}