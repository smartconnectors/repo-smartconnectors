import { Component, OnInit, Input, Output, EventEmitter } from '@angular/core';

import * as d3 from "d3";

@Component({
  selector: 'angular-lender-line',
  templateUrl: './angular-lender-line.component.html',
  styleUrls: ['./angular-lender-line.component.scss']
})
export class AngularLenderLineComponent implements OnInit {

  @Input() autoMap: boolean;

  @Input() set outputOperationData(value) {
    if (value) {
      value.forEach((element, index) => {
        this.outputOperations.push({
          id: index,
          name: element.name,
          type: element.type
        });
      });

      this.filtered = this.outputOperations;
      this.outputOperationsViewModel = Object.assign([], this.outputOperations);

      setTimeout(() => {
        this.automap();
      }, 500);


    } else {
      this.outputOperations = [];
    }
  }

  @Input() set inputOperationData(value) {
    if (value) {
      value.forEach((element, index) => {
        this.inputOperations.push({
          id: index,
          name: element.name,
          type: element.type
        });
      });

      this.inputOperationsViewModel = Object.assign([], this.inputOperations);
      setTimeout(() => {
        this.automap();
      }, 500);
    }
  }

  @Output() mappingChanged = new EventEmitter();

  name = 'Angular';
  inputOperations = [];
  outputOperations = []
  inputOperationsViewModel = [];
  outputOperationsViewModel = []
  mappedInputObj = {};
  mappedOutputObj = {};
  inputSelection;
  outputSelection;
  mapped: boolean = false;
  el;
  el2;
  srctargetMappings = [];
  savedMappings = [];
  manualMappings = [];
  selectIds = [];
  filtered = [];
  searchterm;

  constructor() {

  }

  ngOnInit(): void {

  }

  getOffset(el) {
    // this function calculates the top, left, right and mid of bounding rect of item elements
    if (!el)
      return;

    const rect = el.getBoundingClientRect();
    return {
      right: rect.right + window.scrollX,
      left: rect.left + window.scrollX,
      top: rect.top + window.scrollY,
      bottom: rect.bottom + window.scrollY,
      mid: rect.height / 2
    };
  }

  drawline(srcelement, destelement) {
    // The function draws a line on svg between source and destination elements
    // First calculate the svg top and then draw lines relative to svg placement
    let svgelement = document.getElementById("linessvg");
    let rect = this.getOffset(srcelement);
    let destrect = this.getOffset(destelement);
    let svgbox = this.getOffset(svgelement);

    // Draw first part of the line-diagonal of the elbow
    d3.select("#linessvg")
      .append("line")
      .attr("x1", 0)
      .attr("x2", 200)
      .attr("y1", rect.bottom - svgbox.top - rect.mid)
      .attr("y2", destrect.bottom - svgbox.top - rect.mid)
      .attr("stroke", "grey");
    // Draw second part of the line -horizontal line
    d3.select("#linessvg")
      .append("line")
      .attr("x1", 200)
      .attr("x2", 300)
      .attr("y1", destrect.bottom - svgbox.top - destrect.mid)
      .attr("y2", destrect.bottom - svgbox.top - destrect.mid)
      .attr("stroke", "grey");
  }

  findelement(nametocompare) {
    // return  the matching element in the destination list
    return this.filtered.filter(function (item) {
      return item.name.toLowerCase() === nametocompare.toLowerCase();
    });
  }

  automap() {
    // Disable the automap function   

    // Find elements matching in destination list and save them in srctargetMappings
    for (let i = 0; i < this.inputOperations.length; i++) {
      var nametocompare = this.inputOperations[i].name;
      let foundmap = this.findelement(nametocompare);
      if (foundmap) {
        foundmap.forEach(element => {
          this.srctargetMappings.push({ src: i, target: element.id });
        });
      }
    }

    // For all elements found and stored in srctargetMappings, draw lines between elements
    for (let i = 0; i < this.srctargetMappings.length; i++) {
      let srcelement = document.getElementById(
        "item-" + this.srctargetMappings[i].src
      );

      let destelement = document.getElementById(
        "alt-item-" + this.srctargetMappings[i].target
      );
      if (destelement) {
        //draw line
        this.drawline(srcelement, destelement);
      }
    }
  }

  refresh() {
    // Refresh button
    //disable AutoMap
    let elem = document.getElementById("autobtn");
    elem.setAttribute("disabled", "false");
    //Remove lines
    d3.selectAll("line").remove();
    //reset the mappings
    this.outputOperations = [];
    this.srctargetMappings = [];
    this.manualMappings = [];

    for (let i = 0; i < 20; i++) {
      let random = Math.floor(Math.random() * 39) + 1;
      this.outputOperations.push({ id: i, name: "item" + random });
    }
    // create a new mapping and assign filtered to the same
    this.filtered = this.outputOperations;
  }

  searchList(listname: string) {
    //disable automap button on search(if you want to)
    //let elem = document.getElementById("autobtn");
    //elem.disabled = true;

    //create a filtered list on filter by search term
    this.filtered = this.outputOperations.filter(
      element => element.name.includes(listname) === true
    );
    console.log(this.manualMappings);
    // Remove auto map lines
    d3.selectAll("line").remove();

    //draw manual mappings
    this.drawMappings(this.manualMappings);

    if (this.srctargetMappings) {
      this.drawMappings(this.srctargetMappings);
    }

    if (this.searchterm === "") {
      console.log("empty");
    }
  }

  startmapping(elem: string) {
    // On click on source while drag, create an array of selected Ids
    let srcelement = document.getElementById("item-" + elem);
    srcelement.classList.add("selected");
    this.selectIds.push(elem);
    console.log(this.selectIds);
  }

  savemapping(elem: string) {
    let destelement = document.getElementById("alt-item-" + elem);
    destelement.classList.add("destselected");
    let src = document.getElementsByClassName("selected");
    console.log(src);

    // On click of destination item, save the mappings and draw lines
    this.selectIds.forEach(element => {
      this.manualMappings.push({ src: element, target: elem });
      let srcelement = document.getElementById("item-" + element);
      this.drawline(srcelement, destelement);
      srcelement.classList.remove("selected");
    });
    this.selectIds = [];
    destelement.classList.remove("destselected");

    this.savedMappings.push({
      inputMapping: this.inputSelection,
      outputMapping: this.outputSelection
    });
    this.mappingChanged.emit(this.savedMappings);
  }

  drawMappings(list) {
    // Draw mappings in an array list defining src and targets
    for (let i = 0; i < list.length; i++) {
      let srcelement = document.getElementById("item-" + list[i].src);

      let destelement = document.getElementById("alt-item-" + list[i].target);
      if (destelement) {
        this.drawline(srcelement, destelement);
      }
    }
  }
}
